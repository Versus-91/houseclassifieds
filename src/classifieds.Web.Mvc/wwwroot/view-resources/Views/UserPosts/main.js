var app = new Vue({
	el: '#app',
	data: {
		category: null,
		sort: 1,
		loading: true,
		loadingDistricts: false,
		msg: 'در حال دریافت اطلاعات....',
		url: '/img/icon.png',
		categoryText: ' ',
		city: null,
		cityText: null,
		district: null,
		districtText: null,
		cities: [],
		districts: [],
		roomsCount: 0,
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
		onDistrictChange: function (e) {
			if (e.target.options.selectedIndex > -1) {
				this.districtText = e.target.options[e.target.options.selectedIndex].text;
			}
		},
		onCityChange: function (e) {
			this.loadingDistricts = true;
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
		},
		search: function (sort) {
			this.page = 1;
			var data = {
				category: this.category, district: this.district, city: this.city, minArea: null,
				maxArea: this.maxArea,
				minPrice: this.minPrice,
				maxPrice: this.maxPrice,
				minRent: this.minRent,
				maxRent: this.maxRent,
				types: this.type,
				featured:this.featured,
				minDeposit: this.minDeposit,
				maxDeposit: this.maxDeposit, };
			var uri = '?' + this.serialize(data);
			var query = '';
			switch (sort) {
				case 1:
					query = "creationTime Desc"
					break;
				case 2:
					if (!!this.categoryText) {
						if (this.categoryText.includes("اجاره") || this.categoryText.includes("گروی")) {
							query = "Rent,Deposit Asc"
						} else {
							query = "Price Asc"
						}
					}
					break;
				case 3: 
					if (!!this.categoryText) {
						if (this.categoryText.includes("اجاره") || this.categoryText.includes("گروی")) {
							query = "Rent,Deposit Desc"
						} else {
							query = "Price Desc"
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
					if (!!sort) {
						this.sort = sort;
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