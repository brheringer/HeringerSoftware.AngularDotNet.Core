import { Injectable } from '@angular/core';
import * as moment from 'moment';

@Injectable()
export class FileSaverService {

  popUpCSV(content: any, document: Document, filenamePrefix: string): void {
    var temp_url = window.URL.createObjectURL(new Blob(["\ufeff", content]));
    var redirectWindow = window.open(temp_url);
    //redirectWindow.document.write('<iframe src="' + temp_url 
    //+ '" frameborder="0" style="border:0; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%;" allowfullscreen></iframe>');
  }

  saveCSV(content: any, document: Document, filenamePrefix: string): void {
    var temp_url = window.URL.createObjectURL(new Blob(["\ufeff", content]));
    var link = document.createElement("a");
    link.setAttribute("href", temp_url);
    link.setAttribute("download", `${filenamePrefix}-${moment.utc().format('YYYYMMDDHHmmss')}.csv`);
    link.innerText = "click";
    link.click();
  }

  saveCSV_OLD(content: any, document: Document, filenamePrefix: string): void {
    //https://stackoverflow.com/questions/695151/data-protocol-url-size-limitations
    //nao funcionou no firefox
    //acho que porque tem limit no tamanho do href
    //mas dizem que no firefox eh ilimitado e no chrome eh limitado
    //de qualquer forma, é uma limitação
    var reader = new FileReader();

    reader.onloadend = function () {    
      var base64 = reader.result.toString() ;
      var link = document.createElement("a");
      link.setAttribute("href", base64);
      link.setAttribute("download", `${filenamePrefix}-${moment.utc().format('YYYYMMDDHHmmss')}.csv`);
      link.click();
      console.log(link);
    };

    reader.readAsDataURL(new Blob([content]));
  }

}
