function IsNullOrEmpty(value) {
    if (value !== undefined && value !== null && value.toString().trim().length > 0) {
        return false;
    }
    return true;
}

function CheckElementExists(element)
{
    if (!IsNullOrEmpty(element))
    {
        if ($(element).length > 0)
        {
            return true;
        }
    }
    return false;

    //if ($("input[name=optionRadio" + currentIndex + "]").length > 0) {
    //    // Radio button actually exists...
    //}
}

function ReJSONValue(data) {
    var value = null;
    if (!IsNullOrEmpty(data)) {
        value = "=" + JSON.stringify(data);
        //value = JSON.stringify(data);
    }
    return value;
}

function ReturnValue(data) {
    var value = null;
    if (!IsNullOrEmpty(data)) {
        value = data.toString().trim();
    }
    return value;
}

function ReturnValueText(element) {
    var value = null;
    if (CheckElementExists(element)) {
        var _value = $(element).val();
        if (!IsNullOrEmpty(_value)) {
            value = _value.toString().trim();
        }
    }
    return value;
}

function ReturnValueInt(element)
{
    var value = 0;
    if (CheckElementExists(element)) {
        var _value = $(element).val();
        if (!IsNullOrEmpty(_value)) {
            value = parseInt(_value);
        }
    }
    return value;
}

function ReturnValueFloat(element) {
    var value = 0.0;
    if (CheckElementExists(element)) {
        var _value = $(element).val();
        if (!IsNullOrEmpty(_value)) {
            value = parseFloat(_value);
        }
    }
    return value;
}

function TotalParseInt(element1, element2) {
    var total = 0;
    var value1 = 0;
    var value2 = 0;
    value1 = ReturnValueInt(element1);
    value2 = ReturnValueInt(element2);
    total = value1 + value2;
    return total;
}

function TotalParseFloat(element1, element2) {
    var total = 0.0;
    var value1 = 0.0;
    var value2 = 0.0;
    value1 = ReturnValueFloat(element1);
    value2 = ReturnValueFloat(element2);
    total = value1 + value2;
    return total;
}

function MinMax(value, min, max) {
    if (parseInt(value) < min || isNaN(parseInt(value)))
        return min;
    else if (parseInt(value) > max)
        return max;
    else return value;
}

function AddClassCss(idOrclass, classcss) {
    if (classcss !== undefined && classcss !== null && classcss.toString().trim().length > 0) {
        if (!$(idOrclass).hasClass(classcss)) {
            $(idOrclass).addClass(classcss);
        }
    }
}

function RemoveClassCss(idOrclass, classcss) {
    if (classcss !== undefined && classcss !== null && classcss.toString().trim().length > 0) {
        if ($(idOrclass).hasClass(classcss)) {
            $(idOrclass).removeClass(classcss);
        }
    }
}

function CheckIsNullOrEmpty(idOrclass, classcss, message) {
    var check = true;
    var _value = '';
    if ($(idOrclass).length > 0) {
        _value = $(idOrclass).val();
        if (_value === undefined || _value === null || _value.toString().trim().length === 0) {
            check = false;
            AddClassCss(idOrclass, classcss);
            $(idOrclass).focus();
            if (message !== undefined && message !== null && message.toString().trim().length > 0) {
                alert(message);
            }
            return false;
        }
    }
    if (check) {
        RemoveClassCss(idOrclass, classcss);
    }
    return check;
}

function CheckIsDate(idOrclass, classcss, message) {
    var check = true;
    _value = $(idOrclass).val();

    check = CheckDate(_value);
    if (!check) {
        AddClassCss(idOrclass, classcss);
        $(idOrclass).focus();
        if (message !== undefined && message !== null && message.toString().trim().length > 0) {
            alert(message);
        }
        return false;
    }
    else {
        RemoveClassCss(idOrclass, classcss);
    }
    return check;
}

function CheckIsNumber(idOrclass, classcss, message) {
    var check = true;
    _value = $(idOrclass).val();
    if (isNaN(_value)) {
        check = false;
        AddClassCss(idOrclass, classcss);
        $(idOrclass).focus();
        if (message !== undefined && message !== null && message.toString().trim().length > 0) {
            alert(message);
        }
        return false;
    }
    if (check) {
        RemoveClassCss(idOrclass, classcss);
    }
    return check;
}

function CheckIsNumberLonHon0(idOrclass, classcss, message) {
    var check = true;
    _value = $(idOrclass).val();
    if (parseFloat(_value.toString().trim()) <= 0) {
        check = false;
        AddClassCss(idOrclass, classcss);
        $(idOrclass).focus();
        if (message !== undefined && message !== null && message.toString().trim().length > 0) {
            alert(message);
        }
        return false;
    }
    if (check) {
        RemoveClassCss(idOrclass, classcss);
    }
    return check;
}

function CheckIsNumberLonHonBang0(idOrclass, classcss, message) {
    var check = true;
    _value = $(idOrclass).val();
    if (parseFloat(_value.toString().trim()) < 0) {
        check = false;
        AddClassCss(idOrclass, classcss);
        $(idOrclass).focus();
        if (message !== undefined && message !== null && message.toString().trim().length > 0) {
            alert(message);
        }
        return false;
    }
    if (check) {
        RemoveClassCss(idOrclass, classcss);
    }
    return check;
}
