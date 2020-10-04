import { Pipe, PipeTransform } from '@angular/core';

//Decodes any HTML escape characters (example: &<char>;)
@Pipe({
  name: 'decodeHtml'
})
export class DecodeHtmlPipe implements PipeTransform {

    transform(value: string) {
        const template = document.createElement('div');
        template.innerHTML = value;
        return template.innerText;
    }

}
