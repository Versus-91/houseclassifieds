Vue.component('app-posts-card', {
	props: ['post'],
	data: function () {
		return {
            phoneNumber: null,
            loading:false
		}
    },
    filters: {
        persianDigit:function (value) {
            const persian = {
                0: '۰',
                1: '۱',
                2: '۲',
                3: '۳',
                4: '۴',
                5: '۵',
                6: '۶',
                7: '۷',
                8: '۸',
                9: '۹'
            }

        let result = value.toString()
        for(let i = 0; i <= 9; i++) {
            result = result.replace(new RegExp(`${i}`, 'g'), persian[i])
        }
            value = result;
        return result
        }
    },
    methods: {
        getNumber: function (e) {
            e.preventDefault();
            this.loading = true;
            axios.post('/api/account/GetPhoneNumber?userId=' + this.post.creatorUserId).then((res) => {
                this.phoneNumber = res.data.result;
                this.loading = false;
            }).catch((err) => this.loading=false);
        }
    },
	computed: {
		diffrenceInDays: function () {
			var t2 = new Date().getTime();
            var t1 = new Date(this.post.creationTime).getTime();
            var days = parseInt((t2 - t1) / (24 * 3600 * 1000));
			if (days > 1 ) {
                return days + " روز پیش"
			}
			return "امروز";
		},
	
		imageSrc: function () {
			return this.post?.images && this.post?.images[0] ? this.post.images[0].path + '?width=600&height=480'
				: '/img/placeholder.png'
        },
        title: function () {
            var title = [];
            if (!!this.post.category) {
                title.push(this.post?.category?.name + " " + this.post?.type?.name);
            }

            title.push(this.post.district?.area?.city?.name + ' , ' + this.post?.district?.name);

            return title.join(" , ");
        },
        price: function () {
            if (this.post.category.name.includes("کرای") || this.post.category.name.includes("گروی") || this.post.category.name.includes("اجاره")) {
                var priceLabel = "";
                if (this.post.deposit > 0) {
                    priceLabel += " پیش پرداخت :  " + this.post.deposit + "دالر";
                }
                if (this.post.rent > 0) {
                    priceLabel += "  اجاره :  " + this.post.rent + "دالر";
                }
                return priceLabel;
            }
            else if (this.post.category.name.includes("فروش") ) {
                return this.post.price + "دالر";
            }
            return "";
        },
        subtitle: function () {
            var subtitle = [];
            if (!!this.post.area && this.post.area > 0) {
                subtitle.push(this.post.area + " متر ");
            }
            if (!!this.post.age && this.post.age > 0) {
                subtitle.push(this.post.age + " سال ساخت  ")
            }
            if (!!this.post.bedroom && this.post.bedroom > 0) {
                subtitle.push(this.post.bedroom + " اتاق")
            }
            return subtitle.join(" , ");
        }
	}
	,
	template: `<div class=" column is-12-mobile is-half-tablet is-one-third-desktop  is-one-third-fullhd">
                                <a class="fill-div has-text-grey" :href="'/ads/'+post.id">
                                    <div class="card card-hover-shadow" >
                                        <div class="card-image">
                                            <figure class="image is-4by3">
                                                <img :src="imageSrc" alt="Placeholder image">
                                                <div class="overlay p-1">
                                                  <span class="mr-5">
                                                    {{diffrenceInDays}}
                                                </span>
                                         
                                                 </div>
                                                <div class="overlay-top-corner-right">
                                                 <span class="icon-text has-text-success" v-if="post.isVerified">
                                                   <span class="icon">
                                                     <i class="fas fa-check-square"></i>
                                                   </span>
                                                   <span></span>
                                                 </span>
                                                 <span class="icon-text has-text-grey" v-if="post.hasMedia">
                                                   <span class="icon">
                                                     <i class="fas fa-camera"></i>
                                                   </span>
                                                   <span class="py-1">{{post?.images.length}}</span>
                                                 </span>
                                                </div>
                                            </figure>
                                        </div>
                                        <div class="card-content">
                                                <h1 class="title is-6">{{title | persianDigit}}</h1>
                                                <h2 class="subtitle is-6">{{subtitle | persianDigit}} <br/>{{price | persianDigit}}</h2>
                                        </div>
										<footer class="card-footer">										  <a v-if="phoneNumber == null"  class="card-footer-item" v-on:click="getNumber">											  <p ><i class="fas has-text-success" :class="{'fa fa-phone':loading == false,'fa-spinner fa-spin':loading == true}"></i>  اطلاعات تماس</p>										  </a>										 <p class="card-footer-item" v-else><i class="fas fa-phone has-text-success"></i>{{phoneNumber}}</p>										</footer>
                                    </div>
                                </a>
                            </div>`
})