﻿(function ($) {
    var _cityService = abp.services.app.city,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#CityCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#CitiesTable');

    var _$rolesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#RolesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _cityService.getAll(filter).done(function (result) {
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
                action: () => _$rolesTable.draw(false)
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
                data: 'image',
                sortable: false,
                render: (data, type, row, meta) => {
                    return !!row.image ? `<img style="height:64px;width:64px;" src="${'/'+row.image}" />` : null;
                }
            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-city" data-id="${row.id}" data-toggle="modal" data-target="#CityEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-city" data-city-id="${row.id}" data-city-name="${row.name}">`,
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

    //    var city = _$form.serializeFormToObject();

    //    abp.ui.setBusy(_$modal);
    //    _cityService
    //        .create(city)
    //        .done(function () {
    //            _$modal.modal('hide');
    //            _$form[0].reset();
    //            abp.notify.info(l('SavedSuccessfully'));
    //            _$rolesTable.ajax.reload();
    //        })
    //        .always(function () {
    //            abp.ui.clearBusy(_$modal);
    //        });
    //});

    $(document).on('click', '.delete-city', function () {
        var cityId = $(this).attr("data-city-id");
        var cityName = $(this).attr('data-city-name');

        deleteRole(cityId, cityName);
    });

    $(document).on('click', '.edit-city', function (e) {
        var Id = $(this).attr("data-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'admin/cities/Edit/' + Id,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#CityEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        })
    });

    abp.event.on('city.edited', (data) => {
        _$rolesTable.ajax.reload();
    });

    function deleteRole(cityId, cityName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                cityName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _cityService.delete({
                        id: cityId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$rolesTable.ajax.reload();
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
        _$rolesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$rolesTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
