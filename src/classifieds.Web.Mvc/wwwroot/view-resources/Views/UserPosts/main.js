var app = new Vue({
	el: '#app',
	data: {
		category: null,
		sort: 1,
		loading: true,
		loadingDistricts: false,
		loadingAreas: false,
		msg: 'در حال دریافت اطلاعات....',
		url: '/img/icon.png',
		categoryText: ' ',
		city: null,
		cityText: null,
		areas: [],
		selectedArea: null,
		selectedAreaName: null,
		district: null,
		districtText: null,
		cities: [],
		districts: [],
		roomsCount: 0,
		hasMedia:false,
		featured: false,
		type: null,
		typeText: null,
		posts: [],
		page: 1,
		minArea: null,
		maxArea: null,
		minPrice: null,
		maxPrice: null,
		minRent: null,
		maxRent: null,
		minDeposit: null,
		maxDeposit: null,
		totalItems: 0,
		showFilters: false,
		uri: '',
	},

	methods: {
		onCategoryChange: function (e) {
			if (e.target.options.selectedIndex > -1) {
				this.categoryText = e.target.options[e.target.options.selectedIndex].text;
			}
		},
		onAreaSelect: function (area) {
			this.selectedArea = area.id;
			this.selectedAreaName = area.name;
			this.district = null;
			axios.get("/api/services/app/district/GetByAreaId?id=" + this.selectedArea )
				.then((res) => {
					var items = res.data.result;
					items.unshift({ name: 'انتخاب محله', id: 0 });
					this.districts = items;
					this.district = this.districts[0]?.id;
				}).catch((err) => {
					this.errors = err;
				});
			this.search();
		},
		onDistrictChange: function (e) {
			if (e.target.options.selectedIndex > -1) {
				this.districtText = e.target.options[e.target.options.selectedIndex].text;
			}
		},
		onCityChange: function (e) {
			this.loadingDistricts = true;
			this.selectedArea = null;
			this.selectedAreaName = null;
			axios.get("/admin/districts/GetByCityId?id="+this.city)
				.then((res) => {
					var items = res.data.result;
					items.unshift({ name: 'مهم نیست', id: 0 });
					this.districts = items;
					this.district = this.districts[0].id;
					this.loadingDistricts = false;
				}).catch((err) => {
					this.errors = err;
					this.loadingDistricts = false;
				});
			axios.get("/api/services/app/area/GetAreaByCityId?cityid=" + this.city)
				.then((res) => {
					this.areas = res.data.result.items;
				}).catch((err) => {
					this.errors = err;
				});
		},
		search: function (sort) {
			this.page = 1;
			var data = {
				category: this.category,
				district: this.district,
				city: this.city,
				zone:this.selectedArea,
				minArea: this.minArea,
				maxArea: this.maxArea,
				minPrice: this.minPrice,
				maxPrice: this.maxPrice,
				minRent: this.minRent,
				maxRent: this.maxRent,
				types: this.type,
				featured: this.featured,
				beds: this.roomsCount,
				minDeposit: this.minDeposit,
				maxDeposit: this.maxDeposit,
			};
			if (this.hasMedia == true) {
				data.hasMedia = true;
            }
			var uri = '?' + this.serialize(data);
			var query = '';
			switch (sort) {
				case 1:
					query = "sorting=creationTime Desc"
					break;
				case 2:
					if (!!this.categoryText) {
						if (this.categoryText.includes("اجاره") || this.categoryText.includes("گروی")) {
							query = "sorting =Rent,Deposit Asc"
						} else {
							query = "sorting=Price Asc"
						}
					}
					break;
				case 3: 
					if (!!this.categoryText) {
						if (this.categoryText.includes("اجاره") || this.categoryText.includes("گروی")) {
							query = "sorting=Rent,Deposit Desc"
						} else {
							query = "sorting=Price Desc"
						}
					}
					break;
			}
			if (!!query) {
				this.uri = uri + '&' + query;
			} else {
				this.uri = uri;
			}
			this.loading = true;
			axios.get("/api/services/app/post/getall" + this.uri + "&SkipCount=0&MaxResultCount=30")
				.then((res) => {
					this.showFilters = false;
					this.loading = false;
					this.totalItems = res.data.result.totalCount;
					this.posts = res.data.result.items;
					console.log(sort);
					if (!!sort) {
						this.sort = sort;
					} else {
						this.sort = 1;
                    }
					
				}).catch((err) => {
					this.loading = false;
					this.errors = err;
				});

		},
		paginate: function (page) {
			this.page = page;
			this.loading = true;
			axios.get("/api/services/app/post/getall" + this.uri + "&SkipCount=" + ((page - 1) * 30) + "&MaxResultCount=30")
				.then((res) => {
					this.posts = res.data.result.items;
					this.loading = false;
				}).catch((err) => {
					this.loading = false;
					this.errors = err;
				});
		},
		serialize : function (obj) {
			var str = [];
			for (var p in obj)
				if (obj.hasOwnProperty(p)) {
					if (obj[p] != null && obj[p] != 0){
						str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
					}
				}
			return str.join("&");
		},

	},
	created: function () {
		var categoryOptions = document.getElementsByClassName("categories")[0].options;
		if (categoryOptions.selectedIndex > -1) {
			this.category = categoryOptions[categoryOptions.selectedIndex].value;
			this.categoryText = categoryOptions[categoryOptions.selectedIndex].text;
		} else {
			this.category = categoryOptions[0].value;
			this.categoryText = categoryOptions[0].text;
		}
		var propertyOptions = document.getElementsByClassName("types")[0].options;
		if (propertyOptions.selectedIndex > -1) {
			this.type = propertyOptions[propertyOptions.selectedIndex].value;
			this.typeText = propertyOptions[propertyOptions.selectedIndex].text;
		} else {
			this.type = propertyOptions[0].value;
			this.typeText = propertyOptions[0].text;
		}
		var query = window.location.search;
		var url = new URL(window.location.href);
		var city = url.searchParams.get("city");
		var district = url.searchParams.get("district");
		if (!!city) {
			axios.get("/admin/districts/GetByCityId?id=" + city)
				.then((res) => {
					var items = res.data.result;
					items.unshift({ name: 'مهم نیست', id: 0 });
					this.districts = items;
					if (!!district) {
						this.district = district;
					} else {
						this.district = this.districts[0].id;
					}
				}).catch((err) => {
					this.errors = err;
				});
			axios.get("/api/services/app/area/GetAreaByCityId?cityid=" + city)
				.then((res) => {
					this.areas = res.data.result.items;
				}).catch((err) => {
					this.errors = err;
				});
		}
		this.uri = query == null || query.length == 0 ? '?' : (query + '&');

		axios.get("/api/services/app/post/getall" + this.uri + "SkipCount=0&MaxResultCount=30")
			.then((res) => {
				this.totalItems = res.data.result.totalCount;
				this.posts = res.data.result.items;
				this.loading = false;
			}).catch((err) => {
				this.loading = false;
				this.errors = err;
			});
		axios.get("/api/services/app/city/getall")
			.then((res) => {
				var items = res.data.result.items;
				items.unshift({ name: 'مهم نیست', id: 0 });
				this.cities = items;
				if (!!city) {
					this.city = city;
				} else {
					this.city = this.cities[0].id;
				}
				this.loading = false;
			}).catch((err) => {
				this.loading = false;
				this.errors = err;
			});
	}

})