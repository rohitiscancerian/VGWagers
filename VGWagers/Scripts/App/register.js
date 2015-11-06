$.validator.unobtrusive.adapters.add("agerangevalidation", ["minage", "maxage"], function (options) {
    options.rules["agerangevalidation"] = options.params;
    options.messages["agerangevalidation"] = options.message;
});

$.validator.addMethod("agerangevalidation", function (value, elements, params) {
    debugger;
    var dateOfBirth = value;
    var arr_dateText = dateOfBirth.split("/");
    day = arr_dateText[0];
    month = arr_dateText[1];
    year = arr_dateText[2];

    var mydate = new Date();
    mydate.setFullYear(year, month - 1, day);

    var maxDate = new Date();
    maxDate.setYear(maxDate.getYear() - 18);

    if (maxDate < mydate) {
        $.validator.messages.agerangevalidation = "Sorry, only persons over the age of 16 can be covered";
        return false;
    }
    return true;
});

$.validator.unobtrusive.adapters.addBool("mandatory", "required");


$(function () {

    $('input[type="date"]').attr('type', 'text');
        
    $(".datepicker").datetimepicker({
        format: 'DD/MM/YYYY',

        showClose: true,

        showClear: true,

        toolbarPlacement: 'bottom'
    });

    $("#registerform").validate({
        rules: {
            txtDOB: { agerangevalidation: true }
        }
    });

    $('#registerform', this).submit(function (e) {
        e.preventDefault();
        if (!$(this).valid()) {
            return false;
        }
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            beforeSend: function () {
                $("#modal-loading-div-background").width($("#modalContent").width());
                $("#modal-loading-div-background").show();
            }
        }).done(function (returnedJSON) {
            $("#modal-loading-div-background").hide();
            if (returnedJSON.success) {
                $('#modal-container').modal('hide');
                window.location.reload(true);
            }
            else {
                if (returnedJSON.msg != null) {
                    var alertBox = $("#alertBox");
                    alertBox.html(returnedJSON.msg);
                    alertBox.css("display", "block");
                }
                $.each(returnedJSON.errors, function (key, val) {
                    var container = $('span[data-valmsg-for="' + key + '"]');
                    container.removeClass("field-validation-valid").addClass("field-validation-error");
                    container.html(val[val.length - 1].ErrorMessage);
                });
                return false;
            }
        });
    });


    //allow validation framework to parse DOM
    $.validator.unobtrusive.parse('#registerform');
});