import { HammerGestureConfig } from '@angular/platform-browser';
import * as Hammer from 'hammerjs';

export class AppHammerConfig extends HammerGestureConfig {
    overrides = <any>{
        swipe: {
            direction: Hammer.DIRECTION_ALL
        }
    }

    buildHammer(el: HTMLElement) {
        return new Hammer(el, {
            touchAction: 'auto'
        });
    }
}