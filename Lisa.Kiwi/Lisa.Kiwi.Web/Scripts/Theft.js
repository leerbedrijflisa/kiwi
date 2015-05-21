$(function () {
    var dt = new Date();
    var maxDate = dt.getFullYear() + '/' + dt.getMonth() + '/' + dt.getDate();
    var timeStop = dt.getHours() + ':' + dt.getMinutes()

    //the +maxDate and +timeStop are to set the maxTime and maxDate to the right dates and times, else they will be one minute/day less.
    $('#DateOfTheft').datetimepicker({
        maxDate: +maxDate,
        dateFormat: 'mm/dd/Y H:i',
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