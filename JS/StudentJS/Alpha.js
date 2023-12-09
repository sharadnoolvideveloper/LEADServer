 
 function AlphaOnly() {
     var key = window.event.keyCode;
     if (!(key >= 65 && key <= 120) && (key != 32 && key != 0))
     {
         window.event.returnValue = false;
     }
    
         

 }



