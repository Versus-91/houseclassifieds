Vue.component('l-map', window.Vue2Leaflet.LMap);
Vue.component('l-tile-layer', window.Vue2Leaflet.LTileLayer);
Dropzone.autoDiscover = false;
Vue.directive('select2', {
    inserted(el) {
        $(el).on('select2:select', () => {
            const event = new Event('change', { bubbles: true, cancelable: true });
            el.dispatchEvent(event);
        });

        $(el).on('select2:unselect', () => {
            const event = new Event('change', { bubbles: true, cancelable: true })
            el.dispatchEvent(event)
        })
    },
});
var app = new Vue({
    el: "#app",
    data: {
        category: null,
        tradeType: null,
        zoom: 13,
        editLocation:false,
        url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        dropZone: {},
        post: null,
        center: L.latLng(34.543896, 69.160652),
        marker: null,
        age: 0,
        price: null,
        rent: null,
        deposit: null,
        room: null,
        district: 0,
        description: '',
        amenities: [],
        images:[],
        categories: [],
        area: null,
        isValid: true,
    },
    computed: {
        validateForm: function () {
            return !!this.district && this.district != 0 && !!this.area;
        },
    },

    methods: {
        addMarker: function (e) {
            if (!!this.marker) {
                this.$refs.map.mapObject.eachLayer((layer) => {
                    if (layer?.dragging != null) {
                        layer.remove();
                    }
                });
                this.marker = L.latLng(e.latlng.lat, e.latlng.lng);
                L.marker(this.marker).addTo(this.$refs.map.mapObject);
            } else {
                this.marker = L.latLng(e.latlng.lat, e.latlng.lng);
                L.marker(this.marker).addTo(this.$refs.map.mapObject);
            }
        },
        setAmenity: function (e, amenity) {
            e.preventDefault()

            var index = this.amenities.findIndex(m => m == amenity);
            if (index > -1) {
                this.amenities.splice(index, 1);
            } else {
                this.amenities.push(amenity);
            }
        },
        postSubmit: function (e) {
            e.preventDefault();
            if (this.category.name.includes("خرید") || this.category.name.includes("فروش")) {
                this.deposit = 0;
                this.rent = 0;
            }
            if (this.category.name.includes("گروی") || this.category.name.includes("اجاره") || this.category.name.includes("کرای")) {
                this.price = 0;
            }
            axios.defaults.headers.common['X-XSRF-TOKEN'] = document.getElementsByName("__RequestVerificationToken")[0].value;
            var request = {
                Id:this.post.id,
                typeId: this.tradeType,
                categoryId: this.category.id,
                description: this.description,
                area: this.area,
                price: this.price,
                rent: this.rent,
                deposit: this.deposit,
                age: this.age,
                amenities: this.amenities,
                districtId: this.district
            };
            if (!!this.marker) {
                request.longitude = this.marker.lng;
                request.latitude = this.marker.lat;
            }
            axios.put('/api/services/app/post/update', request).then((res) => {
                window.history.back();
            }).catch((err) => err );
        },
    },

    created() {
        var propertyOptions = document.getElementsByClassName("types")[0];
        if (!!propertyOptions) {
            this.tradeType = propertyOptions.value;
        }
        axios.get('/api/services/app/category/getall').then((res) => {
            this.categories = res.data.result.items;
            this.category = this.categories[0];
        });
    },
    mounted() {
        var id = location.href.substring(location.href.lastIndexOf('/') + 1);
        axios.get('/api/services/app/post/getdetails?id=' + id).then((res) => {
            this.post = res.data.result;
            this.category = { id: this.post.category?.id, name: this.post.category?.name };
            this.age = this.post.age;
            this.area = this.post.area;
            this.room = this.post.bedroom;
            this.price = this.post.price;
            this.tradeType = this.post?.typeId;
            this.rent = this.post.rent;
            this.images = this.post.images;
            this.deposit = this.post.deposit;
            this.district = this.post.districtId;
            this.amenities = this.post.amenities.map((item)=>item.id);
            this.description = this.post.description;
            if (!!this.post.latitude) {
                this.marker = L.latLng(this.post.latitude, this.post.longitude);
                L.marker(this.marker).addTo(this.$refs.map.mapObject);
            }
            let img = this.images;
            let id = this.post.id;
            this.dropZone = new Dropzone('#dropzone', {
                autoProcessQueue: true,
                maxFiles: 8,
                preventDuplicates: true,
                acceptedFiles: 'image/*',
                maxFilesize: 4,
                previewTemplate: document.querySelector('#tpl').innerHTML,
                //addRemoveLinks: true,
                removeFile: function (item) {
                    console.log(item);
                    return null;
				},
                init: function () {
                    var myDropzone = this;
                    
                    if (img.length > 0) {
                        img.forEach((item) => {
                            let mockFile = { name: item.name, size: item.size, serverID: item.id, dataURL:"/"+item.path };
                            myDropzone.files.push(mockFile);
                            myDropzone.emit("addedfile", mockFile);
                            myDropzone.createThumbnailFromUrl(mockFile,
                                myDropzone.options.thumbnailWidth,
                                myDropzone.options.thumbnailHeight,
                                myDropzone.options.thumbnailMethod, true, function (thumbnail) {
                                myDropzone.emit('thumbnail', mockFile, thumbnail);
                            });
                            myDropzone.emit("complete", mockFile);
                        });
                    
					}
                    this.on('error', function (file, errorMessage) {
                        if (errorMessage.indexOf('big') !== -1) {
                            var errorDisplay = document.querySelectorAll('[data-dz-errormessage]');
                            errorDisplay[errorDisplay.length - 2].innerHTML = 'حجم فایل زیاد است.' + '<br/>حداکثر حجم فایل مجاز :' + this.options.maxFilesize + 'مگابایت ';
                        }
                    });
                },
                url: "/upload/"+id,
                headers: {
                    'X-XSRF-TOKEN': document.getElementsByName("__RequestVerificationToken")[0].value
                },
                removedfile: function (file) {
                    if (!!file.serverID) {
                        axios.defaults.headers.common['X-XSRF-TOKEN'] = document.getElementsByName("__RequestVerificationToken")[0].value;
                        axios.post('/upload/remove/' + file.serverID).then((res) => {
                            file.previewElement.remove();
                            return true;
                        }).catch((err) =>  false);
                    } else {
                        file.previewElement.remove();
                        return true;
                    }
                },
                success: function (file, response) {
                   // location.reload();

                }
            });
            //remove duplcate files
            this.dropZone.on("addedfile", function (file) {
                if (this.files.length) {
                    var _i, _len;
                    for (_i = 0, _len = this.files.length; _i < _len - 1; _i++) // -1 to exclude current file
                    {
                        if (this.files[_i].name === file.name && this.files[_i].size === file.size && this.files[_i].lastModifiedDate.toString() === file.lastModifiedDate.toString()) {
                            this.removeFile(file);
                        }
                    }
                }
            });
        });


    },
});
