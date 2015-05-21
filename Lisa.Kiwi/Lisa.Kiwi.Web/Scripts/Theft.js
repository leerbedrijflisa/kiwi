$(function () {
    var dt = new Date();
    var maxDate = dt.getFullYear() + '/' + dt.getMonth() + '/' + dt.getDate();

    //Default for today is 0
    $('#DateOfTheft').datetimepicker({
        maxDate: 0,
        dateFormat: 'mm/dd/Y H:i',
        defaultTime: 0,
        maxTime: 0,
        step: 1,
        lang: 'nl',
        timepickerScrollbar: false,
        onChangeDateTime: function (dp)
        {
            dateCheck = dp.getFullYear() + '/' + dp.getMonth() + '/' + dp.getDate();
            if (dateCheck == maxDate)
            {
                this.setOptions({ maxTime:  0});
            }
            if(dateCheck != maxDate)
            {
                this.setOptions({ maxTime: '23:59' });
            }
        }
    });
});