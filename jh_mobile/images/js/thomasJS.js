
function show(){
  if(document.getElementById("left").style.display=='none'){ //判斷目前左邊欄的狀態（顯示、隱藏）
      document.getElementById("left").style.display='block'; //顯示左邊欄
    }
    else{
      document.getElementById("left").style.display='none'; //隱藏左邊欄
    }
}

//另開視窗並可回傳值
function ReturnValue(jjj) {
     for(var i = 0; i < jjj; i++) {
         var v_str  = "RadioButtonList1_"+i;
		 if(document.getElementById(v_str).checked) {
		    //alert(document.getElementById("RadioButtonList1_"+i).value);		
	        returnValue=document.getElementById("RadioButtonList1_"+i).value;
            close();
		  }
	    }       
}

//全選、取消checkBox        
function CA(){
var frm=document.aspnetForm;
for (var i=0;i<frm.elements.length;i++)
 {
   var e=frm.elements[i];
   if ((e.name != 'ctl00_ContentPlaceHolder1_GV_unGrant_ctl01_allbox') && (e.type=='checkbox'))
  {
    e.checked=frm.ctl00_ContentPlaceHolder1_GV_unGrant_ctl01_allbox.checked;
    if (frm.ctl00_ContentPlaceHolder1_GV_unGrant_ctl01_allbox.checked)
    {
     hL(e);
    }//endif
    else
    {
     dL(e);
    }//endelse

  }//endif
 }//endfor
}


//判斷所有的CHECKBOX是否全部選取
function CCA(CB)
{
  var frm=document.aspnetForm;
  if (CB.checked)
  hL(CB);
  else
  dL(CB);
  
  HighlightRow(CB) 

var TB=TO=0;
for (var i=0;i<frm.elements.length;i++)
{
  var e=frm.elements[i];
  if ((e.name != 'ctl00_ContentPlaceHolder1_GV_unGrant_ctl01_allbox') && (e.type=='checkbox'))
   {
    TB++;
    if (e.checked)
    TO++;
   }
 }
 frm.ctl00_ContentPlaceHolder1_GV_unGrant_ctl01_allbox.checked=(TO==TB)?true:false;
}




function hL(E){
while (E.tagName!="TR")
{E=E.parentElement;}
E.className="H";
}

function dL(E){
while (E.tagName!="TR")
{E=E.parentElement;}
E.className="";
}

//CHECKBOX選取變色
function HighlightRow(chkB) 

{

var IsChecked = chkB.checked;            

if(IsChecked)

  {

       chkB.parentElement.parentElement.style.backgroundColor='#ff0000';  

       chkB.parentElement.parentElement.style.color='black'; 

  }else 

  {

       chkB.parentElement.parentElement.style.backgroundColor='white'; 

       chkB.parentElement.parentElement.style.color='black'; 
      
  }
  

}

//CHECKBOX全選
function SelectAllCheckboxesSpecific(spanChk)

       {

           var IsChecked = spanChk.checked;

           var Chk = spanChk;

              Parent = document.getElementById('ctl00_ContentPlaceHolder1_GV_unGrant');           

              var items = Parent.getElementsByTagName('input');                          

              for(i=0;i<items.length;i++)

              {                

                  if(items[i].id != Chk && items[i].type=="checkbox")

                  {            

                      if(items[i].checked!= IsChecked)

                      {     

                          items[i].click();     

                      }
                      else
                      {
                         items[i].checked = false;
                      
                      }

                  }

              }             

       }

