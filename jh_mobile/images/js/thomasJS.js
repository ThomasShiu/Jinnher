
function show(){
  if(document.getElementById("left").style.display=='none'){ //�P�_�ثe�����檺���A�]��ܡB���á^
      document.getElementById("left").style.display='block'; //��ܥ�����
    }
    else{
      document.getElementById("left").style.display='none'; //���å�����
    }
}

//�t�}�����åi�^�ǭ�
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

//����B����checkBox        
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


//�P�_�Ҧ���CHECKBOX�O�_�������
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

//CHECKBOX����ܦ�
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

//CHECKBOX����
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

