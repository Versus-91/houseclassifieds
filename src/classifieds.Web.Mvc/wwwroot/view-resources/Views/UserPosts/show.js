$(function () {
    var swiper = new Swiper('#imageSlider', {
        // Enable lazy loading
        lazy: true,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

    });

    var swiper2 = new Swiper('#recommendations', {
        slidesPerView: 1,
        spaceBetween: 10,
        // init: false,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        breakpoints: {
            640: {
                slidesPerView: 1,
                spaceBetween: 20,
            },
            768: {
                slidesPerView: 4,
                spaceBetween: 40,
            },
            1024: {
                slidesPerView: 4,
                spaceBetween: 50,
            },
        }
    });
    if (!!$('#postLatitude').val() && !!$('#postLongitude').val()) {
        var map = L.map('map').setView(L.latLng($('#postLatitude').val(), $('#postLongitude').val()), 13);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);

        L.marker([$('#postLatitude').val(), $('#postLongitude').val()]).addTo(map)
            .bindPopup('محل انتخاب شده .');
    }
});
new Vue({
    el: '#app',
    data: {
        showModal: false,
        showShareModal: false,
        showImageModal: false,
        options: [],
        option: null,
        liked: false,
        likeId: null,
        description: '',
        id: null,
        phoneNumber: null,
        loadingNumber:false,
        creatorId: null,
        image:'',
    },

    methods: {
        openGallery: function (e) {
            e.preventDefault();
            console.log(e.target.src);
            this.image = e.target.src.split("?")[0];
            this.showImageModal = true;
        },
        getNumber: function (e) {
            e.preventDefault();
            if (this.phoneNumber == null) {
                this.loadingNumber = true;
                axios.post('/api/account/GetPhoneNumber?userId=' + this.creatorId).then((res) => {
                    this.phoneNumber = res.data.result;
                    this.loadingNumber = false;
                }).catch((err) => this.loadingNumber = false);
            }

        },
        socialWindow: function () {
            var left = (screen.width - 570) / 2;
            var top = (screen.height - 570) / 2;
            var params = "menubar=no,toolbar=no,status=no,width=570,height=570,top=" + top + ",left=" + left;
            window.open(url, "NewWindow", params);
        },
        socialShare: function (media) {
            var pageUrl = encodeURIComponent(document.URL);
            switch (media) {
                case 'twitter':
                    url = "https://twitter.com/intent/tweet?url=" + pageUrl;
                    this.socialWindow(url);
                    break;
                case 'facebook':
                    url = "https://www.facebook.com/sharer.php?u=" + pageUrl;
                    this.socialWindow(url);
                    break;
                default:
                    break;
            }
        },
        addFavorite() {
            if (!!this.liked) {
                this.removeFavorite();
            } else {
                axios.defaults.headers.common['X-XSRF-TOKEN'] = document.getElementsByName("__RequestVerificationToken")[0].value;
                axios.post('/api/services/app/favorite/create', {
                    postId: this.id,
                }).then((res) => {
                    this.liked = true;
                    this.likeId = res.data.result.id;
                }).catch((err) => {
                    if (err.response.status === 401) {
                        location.href = '/account/register';
                    }
                });
            }
        },
        removeFavorite() {
            axios.defaults.headers.common['X-XSRF-TOKEN'] = document.getElementsByName("__RequestVerificationToken")[0].value;
            axios.delete('/api/services/app/favorite/delete?id=' + this.likeId).then((res) => {
                this.liked = false;
                this.likeId = null;
            });
        },
        submitForm() {
            axios.defaults.headers.common['X-XSRF-TOKEN'] = document.getElementsByName("__RequestVerificationToken")[0].value;
            axios.post('/api/services/app/report/create', {
                postId: this.id,
                description: this.description,
                ReportOptionId: this.option,
            }).then((res) => {
                this.description = '',
                    this.option = this.options[0]?.id,
                    this.showModal = false
            });
        }
    },
    created() {
        this.id = document.getElementById('id').value;
        this.creatorId = document.getElementById('userId').value;
        axios.get('/api/services/app/reportoption/getall').then((res) => {
            this.options = res.data.result.items;
            if (!!this.options) {
                this.option = this.options[0].id;
            }
        });

        axios.get('/api/services/app/favorite/GetFavorite?postid=' + this.id).then((res) => {
            this.liked = res.data.result.isLiked;
            this.likeId = res.data.result.id;
        });
    }
});