import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'liqImgUrl'
})
export class LiqImgUrlPipe implements PipeTransform {
    transform(value: string): string {
        if (value == null)
            return '/assets/r6-logo.png';

        return `/api/metadata/image?url=${value}`;
    }
}
