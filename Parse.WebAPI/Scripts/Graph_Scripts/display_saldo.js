function displaySaldo(days, monthDivName) {
    var monthDiv = document.getElementById(monthDivName);
    var monthSaldo = generateMonthSaldo(days);
    monthDiv.appendChild(monthSaldo);
}

function generateMonthSaldo(days) {
    var monthSaldo = document.createElement('DIV');
    monthSaldo.className = "saldo-container"

    for (var i = 0; i < days.length; i++) {
        var saldoOfTheDay = days[i].saldo * 60;

        var daySaldo = generateDaySaldo(saldoOfTheDay);

        monthSaldo.appendChild(daySaldo);
    }

    return monthSaldo;
}

function generateDaySaldo(saldoOfTheDay) {

    var daySaldo = document.createElement('canvas');
    daySaldo.className = "positive-saldo";

    if (saldoOfTheDay < 0)      
        daySaldo.className = "negative-saldo";    
    else
        daySaldo.className = "positive-saldo";

    var saldoIntrvalWidth = Math.abs(saldoOfTheDay);
    daySaldo.style.width = (saldoIntrvalWidth / 1.4).toFixed(0) + 'px'
    var tooltip = document.createElement('canvas')

    daySaldo.onmousemove = function ()
    {      
        tooltip.style.left = event.pageX + 15, top = event.pageY + 15;
        tooltip.className = "tooltip";
        tooltip.style.backgroundColor = 'rgb(0, 0, 0)'
        tooltip.textContent = (Math.floor(saldoOfTheDay / 60).toFixed(0) + 'h ' + Math.abs(saldoOfTheDay % 60).toFixed(0) + 'm');       
        daySaldo.appendChild(tooltip);
        //daySaldo.style.backgroundColor = 'rgb(0, 0, 0)'
        //daySaldo.textContent = (Math.floor(saldoOfTheDay / 60) + 'h' + Math.abs(saldoOfTheDay % 60) + 'm');
    }

    daySaldo.onmouseout = function ()
    {
        daySaldo.textContent = '';
        if (daySaldo.className == "positive-saldo")

            daySaldo.style.backgroundColor = 'rgb(205, 146, 78)';

        else

            daySaldo.style.backgroundColor = 'rgb(185, 38, 54)';
    }
    return daySaldo;
}
