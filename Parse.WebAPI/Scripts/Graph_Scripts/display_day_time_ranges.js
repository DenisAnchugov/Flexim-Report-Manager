function displayData(days, monthDivName) {
    var monthDiv = document.getElementById(monthDivName);
    var month = generateMonth(days);
    monthDiv.appendChild(month);
}

function generateMonth(days) {
    var month = document.createElement('DIV');
    var monthDates = document.createElement('DIV');
    monthDates.className = 'dates';
    var monthCheckINs = document.createElement('DIV');
    monthCheckINs.className = 'intervals';
    month.className = "checkin-ranges-container";
    
    
    for (var i = 0; i < days.length; i++) {       
        var dateDiv = document.createElement('DIV')        
        dateDiv.textContent = days[i].stringDate;
        dateDiv.className = 'date';                       
        monthDates.appendChild(dateDiv);
        
    }

    for (var i = 0; i < days.length; i++) {
        var intervalsOfTheDay = days[i].checkIns;       
        var day = generateDay(intervalsOfTheDay);        
        monthCheckINs.appendChild(day);
    }
    month.appendChild(monthDates);
    month.appendChild(monthCheckINs);

    return month;
}

function generateDay(intervals) {
    var day = document.createElement('DIV');
    day.className = 'day';

    for (var i = 0; i < intervals.length; i++) {        
        var intrvalWidth = intervals[i].checkInRange;
        var intrvalClassName = intervals[i].rangeType;              
        var interval = generateInterval(intrvalWidth, intrvalClassName);

        day.appendChild(interval);
    }

    return day;
}

function generateInterval(width, className) {
    var intervalDiv = document.createElement('canvas');
    intervalDiv.style.width = width/1.4 + 'px';
    intervalDiv.className = className;

    if (intervalDiv.className == 'WorkTime' || intervalDiv.className == 'LunchTime')
    {
        var tooltip = document.createElement('DIV')
        intervalDiv.onmousemove = function () 
        {           
            tooltip.className = "tooltip";
            tooltip.style.left = event.pageX + 15, top = event.pageY + 15;
            tooltip.className = "tooltip";
            tooltip.style.backgroundColor = 'rgb(0, 0, 0)'            
            tooltip.textContent = (Math.floor(width / 60) + 'h ' + Math.abs(width % 60) + 'm');
            //intervalDiv.style.backgroundColor = 'rgb(0, 0, 0)'
            //intervalDiv.textContent = (Math.floor(width / 60) + 'h' + Math.abs(width % 60) + 'm');
            intervalDiv.appendChild(tooltip);
        }
    }
    if (className == 'WorkTime') {

            intervalDiv.style.backgroundColor = 'rgb(205, 146, 78)';
        }

        else if (className == 'LunchTime') {

            intervalDiv.style.backgroundColor = 'rgb(185, 38, 54)';
        }
        else { }
    
    intervalDiv.onmouseout = function ()
    {
        intervalDiv.textContent = '';        
    } 
    return intervalDiv;
}
