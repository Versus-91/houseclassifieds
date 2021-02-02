(function ($) {

    var _amenityService = abp.services.app.amenity,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#CreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#AmenitiesTable');

    var _$amenitiesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#RolesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _amenityService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$amenitiesTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'name',
                sortable: false
            },
            {
                targets: 2,
                data: 'description',
                sortable: false
            },
            {
                targets: 3,
                data: 'icon',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.icon != null  ?
                     `<image src="/${row.icon}"  style="width:3rem;height:2rem;"></image>`:"";
	         }
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-amenity" data-id="${row.id}" data-toggle="modal" data-target="#AmenityEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-amenity" data-amenity-id="${row.id}" data-amenity-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    //_$form.find('.save-button').on('click', (e) => {
    //    e.preventDefault();

    //    if (!_$form.valid()) {
    //        return;
    //    }

    //    //var city = _$form.serializeFormToObject();
    //    var form = $('form')[0]; // You need to use standard javascript object here
    //    var city = new FormData(form);

    //    abp.ui.setBusy(_$modal);
    //    $.ajax({
    //        url: "/admin/amenities/create", method: "POST",data:city})
    //        .done(function () {
    //            _$modal.modal('hide');
    //            _$form[0].reset();
    //            abp.notify.info(l('SavedSuccessfully'));
    //            _$amenitiesTable.ajax.reload();
    //        })
    //        .fail(function () {
    //            alert("error");
    //        })
    //        .always(function () {
    //            abp.ui.clearBusy(_$modal);
    //     });

    //});

    $(document).on('click', '.delete-amenity', function () {
        var cityId = $(this).attr("data-amenity-id");
        var cityName = $(this).attr('data-amenity-name');

        deleteRole(cityId, cityName);
    });

    $(document).on('click', '.edit-amenity', function (e) {
        var Id = $(this).attr("data-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'admin/amenities/Edit/' + Id,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#AmenityEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        })
    });

    abp.event.on('amenity.edited', (data) => {
        _$amenitiesTable.ajax.reload();
    });

    function deleteRole(cityId, cityName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                cityName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _amenityService.delete({
                        id: cityId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$amenitiesTable.ajax.reload();
                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$amenitiesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$amenitiesTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
