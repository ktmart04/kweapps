$(function () {

    var pop = $("#MainContent_LinkToShowProducts3");
    var pop2 = $("#MainContent_DivReceiptProductItems");


    $(document).ready(function () {       
        $(pop).click(function () {
            $(pop2).toggle();          
        });
    });

    $(document).ready(function () {
        //open popup
        $('#MainContent_popKwe').click(function () {
            $('#MainContent_overlay').fadeIn(1000);
            $('#MainContent_example').hide(1000);
            $('#MainContent_background_overlay').fadeIn(500);
            positionPopup();
        });

        //close popup
        $('#MainContent_close, #MainContent_background_overlay').click(function () {
            $('#MainContent_overlay').fadeOut(500);
            $('#MainContent_background_overlay').fadeOut(500);
        });
    });

    //position the popup at the center of the page
    function positionPopup() {
        if (!$('#MainContent_overlay').is(':visible')) {
            return;
        }
        $('#MainContent_overlay').css({
            left: ($(window).width() - $('#MainContent_overlay').width()) / 2,
            top: ($(window).width() - $('#MainContent_overlay').width()) / 7,
            position: 'absolute'
        });
    }

    //maintain the popup at center of the page when browser resized
    $(window).bind('resize', positionPopup);


    //example of 
    
    

    /*
    var $item = $("#MainContent_ValueDescription");


   $($item).hover(function () {
        alert("You entered p1!");
    },
   function () {
       alert("Bye! You now leave p1!");
   });
   */



    //Description Text for each product item textbox..
    $('#MainContent_TextBoxTNEWdesc')
   .focus(function () {
       if ($(this).val() == 'Description') {
           $(this).val('');
       }
   })
  .blur(function () {
      if ($(this).val() == '') {
          $(this).val('Description');
      }
  });

    $('#MainContent_TextBoxTNEWname')
   .focus(function () {
       if ($(this).val() == 'Product Name') {
           $(this).val('');
       }
   })
  .blur(function () {
      if ($(this).val() == '') {
          $(this).val('Product Name');
      }
  });

    $('#MainContent_TextBoxTNEWquan')
.focus(function () {
    if ($(this).val() == 'Quantity Amount') {
        $(this).val('');
    }
})
.blur(function () {
    if ($(this).val() == '') {
        $(this).val('Quantity Amount');
    }
});

    $('#MainContent_TextBoxTNEWunit')
.focus(function () {
    if ($(this).val() == 'Unit Price') {
        $(this).val('');
    }
})
.blur(function () {
    if ($(this).val() == '') {
        $(this).val('Unit Price');
    }
});

    $('#MainContent_TextBoxTNEWpcode')
.focus(function () {
    if ($(this).val() == 'Product Code') {
        $(this).val('');
    }
})
.blur(function () {
    if ($(this).val() == '') {
        $(this).val('Product Code');
    }
});

    function AnotherFunction() {
        alert("This is another function");
    }

    // jQuery methods go here...

});




function changeText(id) {
    id.innerHTML = "Ooops!";
    //ctl00$MainContent$LinkToShowProducts3.style.display = '';

}

function AnotherFunction() {
    alert("Item has been added to this Receipt");
}


 

 
