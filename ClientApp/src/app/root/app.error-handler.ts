import { ToastrService } from 'ngx-toastr';
import { ErrorHandler, Injectable, NgZone } from "@angular/core";

@Injectable()

export class AppErrorHandler implements ErrorHandler {

	constructor(private ngZone: NgZone, private toastr: ToastrService) { }

	handleError(error: any): void {
		this.ngZone.run(() => {
			this.toastr.error('An error occured');
		});
	}
}