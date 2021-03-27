	$(function () {
		function matchCustom(params, data) {
			// If there are no search terms, return all of the data
			if ($.trim(params.term) === '') {
				return data;
			}

			// Do not display the item if there is no 'text' property
			if (typeof data.text === 'undefined') {
				return null;
			}

			// `params.term` should be the term that is used for searching
			// `data.text` is the text that is displayed for the data object
			if (data.text.indexOf(params.term) > -1) {
				var modifiedData = $.extend({}, data, true);
				// You can return modified objects from here
				// This includes matching the `children` how you want in nested data sets
				return modifiedData;
			}

			// Return `null` if the term should not be displayed
			return null;
		}
			 $('.select2').select2({
		theme: 'bootstrap4',
				matcher: matchCustom,
				language: {
		"noResults": function () { return 'موردی پیدا نشد.'; }
				}
			});
		   $('#selectCity').on('select2:select', function (e) {
		  if (e.target.value != 0) {
		$("#selectArea").prop("disabled", false);
			  $.get('/api/services/app/area/GetAreaByCityId?cityid=' + e.target.value, function (res) {
				  var items = res.result.items.map(m => {
					  return {
		id: m.id,
						  text: m.name,
					  }
				  }
				  );
				  items.splice(0, 0, {id: 0, text: "انتخاب ناحیه" })
				  $("#selectArea").empty();
				  $("#selectArea").select2({data: items,theme: 'bootstrap4', });
			  })
		  } else {
		$("#selectArea").empty();
			  $("#selectArea").select2({data: [{id: 0,text: "انتخاب ناحیه" }],theme: 'bootstrap4', });
			  $("#selectArea").prop("disabled", true);
		  }
		});
		$('#selectArea').on('select2:select', function (e) {
		  if (e.target.value != 0) {
		$("#selectDistrict").prop("disabled", false);
			  $.get('/api/services/app/district/GetByAreaId?id=' + e.target.value, function (res) {
				  var items = res.result.map(m => {
					  return {
		id: m.id,
						  text: m.name
					  }
				  }
				  );
				  items.splice(0, 0, {id: 0, text: "انتخاب محله" })
				  $("#selectDistrict").empty();
				  $("#selectDistrict").select2({data: items,theme: 'bootstrap4' });
			  })
		  } else {
		$("#selectDistrict").empty();
			  $("#selectDistrict").select2({data: [{id: 0,text: "انتخاب محله" }],theme: 'bootstrap4' });
			  $("#selectDistrict").prop("disabled", true);
		  }
		});
		});
		Vue.directive('select2', {
		inserted(el) {
		$(el).on('select2:select', () => {
			const event = new Event('change', { bubbles: true, cancelable: true });
			el.dispatchEvent(event);
		});

		$(el).on('select2:unselect', () => {
			const event = new Event('change', {bubbles: true, cancelable: true })
			el.dispatchEvent(event)
		})
	},
});
		new Vue({
		el:"#app",
		   data:{
		name:'',
				owner:'',
				district:null,
				address:'',
				previewImage:null,
				file:null,
				newNumber:'',
				numbers:[]
			},
			computed:{
		isFormValid:function(){
				  return !!this.owner && !!this.address && !!this.district && !!this.name && this.numbers.length >0;
			  },
			},
			methods:{
		addNumber:function(){
		this.numbers.push(this.newNumber);
					this.newNumber='';
				},
				removeNumber:function(number){
		let index = this.numbers.findIndex(m=> m == number);
					if(index > -1){
		this.numbers.splice(index, 1);
					}
				},
				submit:function(number){
		axios.defaults.headers.common['X-XSRF-TOKEN'] = document.getElementsByName("__RequestVerificationToken")[0].value;
					var form = new FormData();
					form.append('name', this.name);
					form.append('owner', this.owner);
					form.append('address', this.address);
					form.append('districtId', this.district);
					form.append('file', this.file);
					form.append('PhoneNumbers', this.numbers.join(','));
					  axios.post('/admin/realestates/create', form).then((res) => {
		window.location.href = "/admin/realestates";
					   }).catch((err) => console.log(err));
				},
			   uploadImage(e){
				const image = e.target.files[0];
				this.file = e.target.files[0];
				const reader = new FileReader();
				reader.readAsDataURL(image);
				reader.onload = e =>{
		this.previewImage = e.target.result;
					console.log(this.previewImage);
				};
			}
			},
		   created:function(){
		console.log('created');
		   }
		});
