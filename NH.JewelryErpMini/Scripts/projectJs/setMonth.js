function setMonth(value) {
    var date = new Date();
    var num = date.getMonth() + 1;
    if ((num + value) % 12 == 0) {
        num = num + value;
    } else {
        num = (num + value) % 12;
    }
    return num;
}