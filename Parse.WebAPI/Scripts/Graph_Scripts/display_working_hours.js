function displayWHRange(days, monthDivName) {
    var monthDiv = document.getElementById(monthDivName);
    var monthWHRange = generateMonthWHRange(days);
    monthDiv.appendChild(monthWHRange);
}

function generateMonthWHRange(days) {
    var monthWHRange = document.createElement('DIV');
    monthWHRange.className = "working-hours-container"
    for (var i = 0; i < days.length; i++) {
        var WHRangeOfTheDay = days[i].workingHoursDay;
        var dayWHRange = generateDayWHRange(WHRangeOfTheDay * 60);
        monthWHRange.appendChild(dayWHRange);
    }
    return monthWHRange;
}

function generateDayWHRange(width) {
    var dayWHRange = document.createElement('canvas');
    dayWHRange.className = "working-hours";
    dayWHRange.style.width = width.toFixed(0) / 1.4 + 'px'
    var tooltip = document.createElement('DIV')

    dayWHRange.onmousemove = function () {
        tooltip.className = "tooltip";
        tooltip.style.left = event.pageX + 10, top = event.pageY + 10;
        tooltip.className = "tooltip";
        tooltip.style.backgroundColor = 'rgb(0, 0, 0)'
        tooltip.textContent = (Math.floor(width / 60).toFixed(0) + 'h ' + Math.abs(width % 60).toFixed(0) + 'm');
        //dayWHRange.style.backgroundColor = 'rgb(0, 0, 0)'
        //dayWHRange.textContent = (Math.floor(width / 60) + 'h ' + Math.abs(width % 60) + 'm');
        dayWHRange.appendChild(tooltip);
    }

    dayWHRange.onmouseout = function () {
        dayWHRange.textContent = '';       
        dayWHRange.style.backgroundColor = 'rgb(205, 146, 78)';
    }

    return dayWHRange;
}


