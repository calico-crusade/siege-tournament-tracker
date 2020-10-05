import { Pipe, PipeTransform } from '@angular/core';

//re-writes image URLs to use the caching service
@Pipe({
  name: 'liqImgUrl'
})
export class LiqImgUrlPipe implements PipeTransform {
    transform(value: string, type?: string): string {
        if (value == null) {
            if (type === 'match')
                return '/assets/r6-logo-compact.png';
            return '/assets/r6-logo.png';
        }

        return `/api/metadata/image?url=${value}`;
    }
}
