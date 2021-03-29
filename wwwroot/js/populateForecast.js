﻿function refreshWeatherForecast() {
    $(document).ready(function () {

        $.ajax({
            url: "/WeatherForecast",
            success: function (result) {
                console.log(result);
                let tomorrow = result[0];
                let tomorrowDate = formatDate(tomorrow.date);

                $("#date").text(tomorrowDate);
                $("#temperature").text(tomorrow.temperatureC, ' C');
                $("#summary").text(tomorrow.summary);
            },
            error: function () {
                console.log('Failed to get data');
            }
        });
    });
}

    setInterval(refreshWeatherForecast, 3000);

    function formatDate(jsonDate) {

        function join(t, a, s) {
            function format(m) {
                let f = new Intl.DateTimeFormat('en', m);
                return f.format(t);
            }
            return a.map(format).join(s);
        }

        let date = new Date(jsonDate);
        let a = [{ day: 'numeric' }, { month: 'short' }, { year: 'numeric' }];
        let s = join(date, a, '-');
        return s;
    }
