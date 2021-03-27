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
        realEstateId: null,
        zoom: 13,
        url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        dropZone: {},
        center: L.latLng(34.543896, 69.160652),
        marker: null,
        age: 0,
        price: null,
        room: null,
        district: 0,
        description: '',
        amenities: [],
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
            axios.defaults.headers.common['X-XSRF-TOKEN'] = document.getElementsByName("__RequestVerificationToken")[0].value;
            var request = {
                typeId: this.tradeType,
                categoryId: this.category.id,
                description: this.description,
                area: this.area,
                price: this.price,
                age: this.age,
                bedroom: this.room,
                amenities: this.amenities,
                districtId: this.district,
                realEstateId: this.realEstateId,
            };
            if (!!this.marker) {
                request.longitude = this.marker.lng;
                request.latitude = this.marker.lat;
            }
            axios.post('/api/services/app/post/create', request).then((res) => {
                console.log(res);
                if (this.dropZone?.files?.length > 0) {
                    this.dropZone.options.url = '/upload/' + res?.data?.result?.id
                    this.dropZone.processQueue();
                } else {
                    window.location.href = "../";
                }

            }).catch((err) => console.log(err));
        },
    },
    watch: {
        category: function () {
            this.price = null;
        }
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
        this.dropZone = new Dropzone('#dropzone', {
            autoProcessQueue: false,
            maxFiles: 8,
            preventDuplicates: true,
            acceptedFiles: 'image/*',
            maxFilesize: 4,
            previewTemplate: document.querySelector('#tpl').innerHTML,
            //addRemoveLinks: true,
            init: function () {
                this.on('error', function (file, errorMessage) {
                    if (errorMessage.indexOf('big') !== -1) {
                        var errorDisplay = document.querySelectorAll('[data-dz-errormessage]');
                        errorDisplay[errorDisplay.length - 2].innerHTML = 'حجم فایل زیاد است.' + '<br/>حداکثر حجم فایل مجاز :' + this.options.maxFilesize + 'مگابایت ';
                    }
                });
            },
            url: "/ads/create",
            headers: {
                'X-XSRF-TOKEN': document.getElementsByName("__RequestVerificationToken")[0].value
            },
            success: function (file, response) {
                window.location.href = "../";
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
    },
});
