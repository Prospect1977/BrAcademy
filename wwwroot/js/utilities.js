var gws = "../WebServices/gws.asmx/";
var googleMapsKey = "AIzaSyBITblKLZ3VExzxdrB37GBBTUmqRi6VFHY";
function getQstring(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);

    var results = regex.exec(window.location.href);

    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

//function CountryUserID(){
//var URL = 'https://extreme-ip-lookup.com/json/';
 

//    $.getJSON(URL, function (data) {

//        $.post(gws + "CountryIDbyCountryAlias", { CountryAlias: data.countryCode }, function (result) {
//            $("#hiddenUserCountryID").val(result);
//        });
       
//    });
//}

//CountryUserID();

//function CountryIDbyCountryAlias(alias) {
//    console.log("alias",alias);
//    $.get('../Countries.txt', function (result) {
//        console.log(result);
//        Countries = $.parseJSON(result);
//       // return Countries.filter(country => country.Alias=alias)[0].CountryID;
//        var Id = Countries.filter(function (country) { return country.Alias = alias })[0];
//        console.log("Id",Id);
//        return Id;
//    });
//}
function formatLargeNumber(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}
function intZeroLead(n){
    return n > 9 ? "" + n : "0" + n;
   // IntZeroLead(9); Returns "09"
   // IntZeroLead(10); Returns "10"
    //IntZeroLead(999);Returns "999"
}
function dateDiff(recentDate, olderDate) {
    var dateFirst = new Date(recentDate);
    var dateSecond = new Date(olderDate);

    // time difference
    var timeDiff = dateFirst.getTime() - dateSecond.getTime();

    // days difference
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));

    // difference
    return diffDays;
}
function TodayDate(){
    var Today = new Date();
  
    var ThisMonth = Today.getMonth() + 1;
    return Today.getDate() + "/" + ThisMonth + "/" + Today.getFullYear();
}




function FullEnglishTodayDate() {
    var Today = new Date();

    var ThisMonth = Today.getMonth() + 1;
    return  ThisMonth + "/" +  Today.getDate() + "/" + Today.getFullYear() + " " +Today.getHours()+":"+Today.getMinutes();
}
function ConvertDateToArabic(MyDate) {
    return MyDate.split("/")[1] + "/" + MyDate.split("/")[0]+ "/" + MyDate.split("/")[2];
}
function ConvertDateToEnglish(MyDate) {
    return MyDate.split("/")[1] + "/" + MyDate.split("/")[0] + "/" + MyDate.split("/")[2];
}
function AddPageVisit(Type, ID) {
    //varURL='https://geoip.nekudo.com/api';
   // var URL='https://api.ipapi.com/5.162.204.54?access_key=de04112ac72cf2d32d3a3d0419170589';

    var URL='https://extreme-ip-lookup.com/json/';
 
    if (Type == "Organization") {
        $.getJSON(URL, function (data) {
            console.log(data);
            $.post(gws + "OrgPageVisit", { OrgID: ID, ip: data.query, country_code: data.countryCode, country_name: data.country }, function () {


            });

        });
        
    } else if (Type == "Project") {
        $.getJSON(URL, function (data) {
            console.log(data);
            $.post(gws + "ProjPageVisit", { ProjID: ID, ip: data.query, country_code: data.countryCode, country_name: data.country }, function () {


            });
        });
    }
    else if (Type == "Product") {
        $.getJSON(URL, function (data) {

            $.post(gws + "SupplierMaterialPageVisit", { SupplierMaterialID: ID, ip: data.query, country_code: data.countryCode, country_name: data.country }, function () {
            });
        });
    }
    else if (Type == "Article") {
        $.getJSON(URL, function (data) {
            $.post(gws + "ArticlePageVisit", { ArticleID: ID, ip: data.query, country_code: data.countryCode, country_name: data.country }, function () {
            });
        });
    }
    else if (Type == "Template") {
        $.getJSON(URL, function (data) {

            $.post(gws + "TemplatePageVisit", { TemplateID: ID, ip: data.query, country_code: data.countryCode, country_name: data.country }, function () {
            });
        });
    }
    else if (Type == "OrgIndividual") {
        $.getJSON(URL, function (data) {
            $.post(gws + "OrgPageVisit", { OrgID: ID, ip: data.query, country_code: data.countryCode, country_name: data.country }, function () {


            });
        });
        }
}
       
function getQstringFromUrl(name, url) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url)
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function formatNumber(x) {
    //  if (x.length > 3) {
    //x = parseInt(x);
    //  }

    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
function formatElementNumber(element) {
    $(element).each(function () {
        $(this).text($(this).text().toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

    });

}
function autoExpand(ObjName) {
    $(ObjName).keyup(function () {

        while (parseFloat($(ObjName).outerHeight()) < parseFloat($(ObjName)[0].scrollHeight + parseFloat($(ObjName).css("borderTopWidth")) + parseFloat($(ObjName).css("borderBottomWidth")))) {
            $(ObjName).height($(ObjName).height() + 1);
        }
    });
}
function autoHeight(ObjName) {
    $(ObjName).each(function (i, obj) {

        var scrollHeight = $(obj)[0].scrollHeight;

        while (parseFloat($(obj).outerHeight()) < parseFloat(scrollHeight + parseFloat($(obj).css("borderTopWidth")) + parseFloat($(obj).css("borderBottomWidth")))) {
            console.log('$(this).outerHeight()' + $(obj).outerHeight());
            console.log('scrollHeight + parseFloat($(this).css("borderTopWidth")) + parseFloat($(this).css("borderBottomWidth"))=' + parseFloat(scrollHeight + parseFloat($(obj).css("borderTopWidth")) + parseFloat($(obj).css("borderBottomWidth"))));

            console.log('$(this)[0].scrollHeight' + $(obj)[0].scrollHeight);
            console.log('parseFloat($(this).css("borderTopWidth"))' + parseFloat($(obj).css("borderTopWidth")));
            console.log('parseFloat($(this).css("borderBottomWidth"))' + parseFloat($(obj).css("borderBottomWidth")));
            $(obj).height($(obj).height() + 1);

        }
    });
};

function shortenText(ObjName, maxLength) {
    $(ObjName).each(function () {
        if ($(this).text().length > maxLength) {
            var OrigText = $(this).text();
            var newText = $(this).text().substr(0, maxLength - 3) + '...';
            $(this).text(newText).attr('title', OrigText).tooltip({ container: 'body' });
        }
    });
}
function shortenTextForString(string, maxLength) {
   
        if (string.length > maxLength) {
           
            return string.substr(0, maxLength - 3) + '...';
        } else {
            return string;
        }

}
function shortenTextAdvanced(ObjName, maxLength) {
    $(ObjName).each(function () {
        if ($(this).text().length > maxLength) {
            var OrigText = $(this).text();
            var newText = OrigText.substr(0, maxLength - 9) + '...' + OrigText.substr(OrigText.length - 5, 5);
            $(this).text(newText).attr('title', OrigText).tooltip({ container: 'body' });
        }
    });
}

function shortenTextAdvancedForString(string, maxLength) {
   
    if (string.length > maxLength) {
        var OrigText = string;
        var newText = OrigText.substr(0, maxLength - 9) + '...' + OrigText.substr(OrigText.length - 5, 5);
        return newText
    }else{
        return string
    }
        
    }


function shortenTextExpandable(ObjName, maxLength) {
    $(ObjName).each(function () {
        if ($(this).text().length > maxLength) {
            var $this = $(this);
            var OrigText = $(this).html();
            var newText = $(this).text().substr(0, maxLength - 3);
            $(this).html(newText);
            var $anchorExpand = $('<a class="anchorExpand">...Continue reading</a>').insertAfter($this);
            $anchorExpand.css({ 'color': 'gray', 'font-weight': '100', 'font-style': 'italic', 'margin-left': '7px', 'cursor': 'pointer' });
            $anchorExpand.click(function () {
                $(this).hide();
                $this.html(OrigText);
            });
        }
    });
}

function defaultKey(DivName, ButtonName) {
    $(DivName).keyup(function (e) {
        if (e.keyCode == 13) {
            $(ButtonName).trigger('click');
        }
    });
}
function cancelKey(DivName, ButtonName) {
    $(DivName).keyup(function (e) {
        if (e.keyCode == 27) {
            $(ButtonName).trigger('click');
        }
    });
}


function addQuestionMark(obj) {
    obj.text('[?]');
}
$(function () {
    $('.toltip').tooltip({ container: 'body' });

});
function colorBadge(obj) {
    $(obj).each(function () {
        var value = $(this).text();
        if (value < 50) {
            $(this).css('background-color', 'gray');
        } else if (value >= 50 && value < 100) {
            $(this).css('background-color', 'orange');
        } else if (value > 100) {
            $(this).css('background-color', 'green');
        }
    });
}
function helpAboutPoints(obj, placement) {
    var pathPrefix = "";
    if ($('#HiddenFileDepth').val() == 1) {
        pathPrefix = "../";
    }
    if ($('#HiddenFileDepth').val() == 2) {
        pathPrefix = "../../";
    }
    $(obj).each(function () {
        $(obj).popover({
            "html": "true", width: '500px', "container": "body", toggle: "popover", trigger: "focus", "placement": placement, "content": "The number of points reflects how active an organization or person inside the market.<br/>" +
                "for example if that organization or person has a verified role in a project, it will gain 5 points multiplied by the project's rank," +
                "also every time that organization or person got rated by another one this will increase its credit of points."
            + "<br><span class='fa fa-square' style='color:gray'></span>  = Credit is less than 50 points"
                + "<br><span class='fa fa-square' style='color:orange'></span> = Credit is between 50 to 99 points"
                + "<br><span class='fa fa-square' style='color:green'></span>  = Credit is 100+ points<br/>" +
                "For more about gaining points rules, click <a style='text-decoration:underline' href='" + pathPrefix + "Article?ID=9171&Context=System'>here</a>."
        });
    });

}


function currencyDescription(obj) {
    $(obj).each(function () {
        var $this = $(this);
        $.post(gws + "CurrencyDescriptionByValue", { Currency: $this.text() }, function (result) {
            $this.attr('title', result).tooltip({ container: 'body' });
        })
    });
}
function isControlEmpty(obj) {


    if ($(obj).val().length == 0) {
        return true;
    } else {
        return false;
    }

}


function detectDirection(obj) {
    var rtlChar = /[\u0590-\u083F]|[\u08A0-\u08FF]|[\uFB1D-\uFDFF]|[\uFE70-\uFEFF]/mg;

    $(function () {
        $(obj).keyup(function () {
            var isRTL = this.value.match(rtlChar);
            if (isRTL !== null) {
                this.style.direction = 'rtl';
            }
            else {
                this.style.direction = 'ltr';
            }
        });
    });
}

function htmlEncode(value) {
    return $('<div>').text(value).html();
}
function htmlDecode(value) {
    return $('<div>').html(value).text();
}

function ajaxErrorMessage(message) {
    //you must avoid using the ' at ' word on the literal error message
    var Jmessage = $.parseJSON(JSON.stringify(message)).responseText;

    Jmessage = Jmessage.replace('System.Exception:', '');
    Jmessage = Jmessage.substr(0, Jmessage.indexOf(' at '));
    return (htmlDecode(Jmessage));
    /*
    Usage example:
    asmx file:
    Throw New Exception("Please give a rate first to be able to switch the status of this notification to seen!")
    javascript:
     $.post("", { parameter: value }, function () {      
       }).fail(function (message) {
        alert(ajaxErrorMessage(message));
      });
      */
}

function PleaseWait(action, message) {
    if (action == 'show') {
        if (message != null) {

        } else {
            message = "Please wait"
        }
        var pathPrefix = "";
        if ($('#HiddenFileDepth').val() == 1) {
            pathPrefix = "../";
        }
        if ($('#HiddenFileDepth').val() == 2) {
            pathPrefix = "../../";
        }
        $('#pleaseWait').detach();
        var element = $('<div id="pleaseWait" style="margin:auto;position:absolute;left:50%;border-radius:7px;border:1px solid lightgray;vertical-align:middle;padding:10px;box-shadow: 0 0 10px #ccc4cc;z-index: 100;background-color: white">' +
            '<img src="' + pathPrefix + 'images/PleaseWait_Gray.gif" /><span style="margin-left:5px">' + message + '</span>' +
        '</div>').appendTo('body').show();
        element.css("top", $(window).scrollTop() + $(window).height() / 2);
    }

    if (action == 'hide') {
        $('#pleaseWait').detach();
    }
}

function PleaseWaitFor(action, seconds, message) {
    if (action == 'show') {
        if (message != null) {

        } else {
            message = "Please wait"
        }
        var pathPrefix = "";
        if ($('#HiddenFileDepth').val() == 1) {
            pathPrefix = "../";
        }
        if ($('#HiddenFileDepth').val() == 2) {
            pathPrefix = "../../";
        }
        $('#pleaseWait').detach();
        var pathPrefix = "../";
        var element = $('<div id="pleaseWait" style="margin:auto;position:absolute;left:50%;border-radius:7px;border:1px solid lightgray;vertical-align:middle;padding:10px;box-shadow: 0 0 10px #ccc4cc;z-index: 100;background-color: white">' +
            '<img src="' + pathPrefix + 'images/PleaseWait_Gray.gif" /><span id="spanPleasewaitMessage" style="margin-left:5px">' + message + '</span>' +
        '</div>')
        element.appendTo('body').show();
        element.css("top", $(window).scrollTop() + $(window).height() / 2);
        if (seconds != null && seconds > 0) {
            var timeleft = seconds;
            element.find("#spanPleasewaitMessage").text(message + " " + timeleft + " s");
            var downloadTimer = setInterval(function () {
                timeleft -= 1;
                if (timeleft <= 0) {
                    clearInterval(downloadTimer);

                } else {
                    element.find("#spanPleasewaitMessage").text(message + " " + timeleft + " s");
                }

            }, 1000);
        }

    }

    if (action == 'hide') {
        $('#pleaseWait').detach();
    }
}



function PleaseWaitMessage(action, message) {
    //......................................................Depricated function
    if (action == 'show') {
        $('#pleaseWait').detach();
        var element = $('<div id="pleaseWait" style="margin:auto;position:absolute;left:50%;border-radius:7px;border:1px solid lightgray;vertical-align:middle;padding:10px;box-shadow: 0 0 10px #ccc4cc;z-index: 100;background-color: white">' +
            '<img src="../images/PleaseWait_Gray.gif" /><span style="margin-left:5px">' + message + '</span>' +
        '</div>').appendTo('body').show();
        element.css("top", $(window).scrollTop() + $(window).height() / 2);
    }

    if (action == 'hide') {
        $('#pleaseWait').detach();
    }
}

function DialogConfirm(title, message, btnTitle, completeFunction) {
    var $dialog = $('<div id="dialog-confirm" style="display:none" title="' + title + '">' +
  '<p><span class="fa fa-warning text-warning" style="float:left; margin:0 7px 0 0;color:orange;line-height: 20px;"></span>' + message + '</p>' +
'</div>');

    $dialog.dialog({
        resizable: false,
        modal: true,
        buttons: [{
            text: btnTitle,
            click: function () {
                completeFunction();
                $(this).dialog("close");
                //$('#dialog-confirm').detach();
            }
        }, {
            text: "Cancel",
            click: function () {
                $(this).dialog("close");
                PleaseWait("hide");
               // $('#dialog-confirm').detach();
            }
        }
        ]
    });

}
function DialogInput(message, inputId, placeholder, width, completeFunction) {
    if (width.length == 0) {
        width = "350px";
    }
    var $dialog = $('<div id="dialog-confirm" style="display:none" title="">' +
  '<p>' + message +
  '<input id="' + inputId + '" class="form-control" type="text" style="margin:15px 0 0 0" placeholder="' + placeholder + '"/>' +
  '</p>' +
'</div>');

    $dialog.dialog({
        resizable: false,
        width: width,
        modal: true,
        buttons: [{
            text: "OK",
            click: function () {

                completeFunction();
                $(this).dialog("close");
            }
        }, {
            text: "Cancel",
            click: function () {
                $(this).dialog("close");
            }
        }
        ]
    });

}
function Panel(type, message, insertAfter, style) {
    if (style == null) { style = ""; }
    $(insertAfter).nextAll('.alert').detach();
    $('<div style="position: relative;padding: 5px;margin: 5px;' + style + '" class="alert alert-' + type + ' alert-dismissible fade in">' +
                       ' <button class="close" type="button" data-dismiss="alert" aria-label="close" style="right: 0"><span aria-hidden="true">&times;</span></button>' +
                        message +
                    '</div>').insertAfter(insertAfter);

}
function PanelAppend(type, message, appendTo, style) {
    if (style == null) { style = ""; }
    $(appendTo).find('.alert').detach();
    $('<div style="position: relative;padding: 5px;margin: 5px;' + style + '" class="alert alert-' + type + ' alert-dismissible fade in">' +
                       ' <button class="close" type="button" data-dismiss="alert" aria-label="close" style="right: 0"><span aria-hidden="true">&times;</span></button>' +
                        message +
                    '</div>').appendTo(appendTo);

}
function htmlEncode(value) {
    return $('<div>').text(value).html();
}
function htmlDecode(value) {
    return $('<div>').html(value).text();
}

function googleDistanceToZoom(distance) {
    var d = parseInt(distance);
    if (d > 17798089) { return 2; }
    else if (d > 8481114) { return 3; }
    else if (d > 4395760) { return 4; }
    else if (d > 2201271) { return 5; }
    else if (d > 1087329) { return 6; }
    else if (d > 544179) { return 7; }
    else if (d > 272322) { return 8; }
    else if (d > 136107) { return 9; }
    else if (d > 68011) { return 10; }
    else if (d > 33998) { return 11; }
    else if (d > 16998) { return 12; }
    else if (d > 8498) { return 13; }
    else if (d > 4238) { return 14; }
    else if (d > 2124) { return 15; }
    else if (d > 1062) { return 16; }
    else if (d > 596) { return 17; }
    else if (d > 100) { return 18; }

}
function ValidateEmpty(control, name) {

    var $control = $(control);
    if ($control.val().length == 0) {
        $control.nextAll(".validator-empty").detach();
        var $validator = $('<span class="validator validator-empty">' + name + ' should not be empty!</span>');
        $validator.appendTo($control.parent());
    } else {
        $control.nextAll(".validator-empty").detach();
    }


}
function ValidateEmptyDropdown(control, name) {

    var $control = $(control);
    if ($control.find("option").length == 0) {
        $control.nextAll(".validator-empty").detach();
        var $validator = $('<span class="validator validator-empty">' + name + ' should not be empty!</span>');
        $validator.appendTo($control.parent());
    } else {
        $control.nextAll(".validator-empty").detach();
    }


}
function ValidateNumber(control) {

    var $control = $(control);
    if (!$.isNumeric($control.val()) && $control.val().length > 0) {
        $control.nextAll(".validator-number").detach();
        var $validator = $('<span class="validator validator-number">You should enter a number!</span>');
        $validator.appendTo($control.parent());
    } else {
        $control.nextAll(".validator-number").detach();
    }
}

function ValidateEmail(control) {

    var $control = $(control);
    if ((! $control.val().includes("@") || ! $control.val().includes(".")) && $control.val().length > 0) {
       
 $control.nextAll(".validator-email").detach();
            var $validator = $('<span class="validator validator-email">The email you entered is not valid!</span>');
            $validator.appendTo($control.parent());
    } else {
        $control.nextAll(".validator-email").detach();          

    }
}


function validateEmailTrueFalse(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

//var allCurrencies = "";
//jQuery.get('../Models/CurrencyRates.txt?i=' + (new Date()).getTime(), function (data) {
//    allCurrencies = data;
//});
//function convertIntoDollar(curr,value) {
   
//    var currencies = allCurrencies.split(",");
//    for (var i = 0; i < currencies.length; i++) {
//        if (currencies[i].split(":")[0] == curr) {
//            return (1 / parseFloat(currencies[i].split(":")[1]))*value;
//        }
//    }
//}
//function convertFromDollar(curr,value) {
//    var currencies = allCurrencies.split(",");
//    for (var i = 0; i < currencies.length; i++) {
//        if (currencies[i].split(":")[0] == curr) {
//            return (parseFloat(currencies[i].split(":")[1]))*value;
//        }
//    }
//}

function serviceNotSubscribedMessage(OrgID, ServiceDescription) {
    return ('<span class="fa fa-warning"></span>&nbsp;Your are not subscribed to the <b>'+ ServiceDescription +'</b> service on the<b><u> <a href="../../Org/OrgManager?ID=' + OrgID + '"> Manager page</a></u></b>! Please send us an <b><u><a href="mailto:info@archilobby.com">Email</a></u></b> to give you a subscription offer.')
}
function  expiredServiceMessage(OrgID, ServiceDescription) {
    return ('<span class="fa fa-warning"></span>&nbsp; Your <b>' + ServiceDescription + '</b> service has expired! Please refer to your <b><u> <a href="../../Org/OrgManager?ID=' + OrgID + '"> Manager page</a></u></b>, as for now,  you may only edit existing records but you can not add new ones.')
}
//going to a specific element
(function ($) {
    $.fn.goTo = function () {
        $('html, body').animate({
            scrollTop: $(this).offset().top + 'px'
        }, 'fast');
        return this; // for chaining...
    }
})(jQuery);

