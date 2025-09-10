function allowOnlyNumbers(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 47 && charCode < 58) {
        return true;
    } 
    else if (charCode == 8 || charCode == 46 || charCode == 9 || charCode == 13 || 
             (charCode >= 37 && charCode <= 40)) {
        return true;

    }
    else {

        return false;
        
    }
}