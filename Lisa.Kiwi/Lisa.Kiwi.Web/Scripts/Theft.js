$(function () {
    var dt = new Date();
    var maxDate = dt.getFullYear() + '/' + dt.getMonth() + '/' + dt.getDate();
    var timeStop = dt.getHours() + ':' + dt.getMinutes()

    $('#DateOfTheft').datetimepicker({
        maxDate: +maxDate,
        format: 'm/d/Y H:i',
        defaultTime: timeStop,
        maxTime: +timeStop,
        step: 1,
        lang: 'nl',
        timepickerScrollbar: false,
        onChangeDateTime: function (dp)
        {
            dateCheck = dp.getFullYear() + '/' + dp.getMonth() + '/' + dp.getDate();
            if (dateCheck == maxDate)
            {
                this.setOptions({ maxTime: +timeStop });
            }
            if(dateCheck != maxDate)
            {
                this.setOptions({ maxTime: '23:59' });
            }
        }
    });
});