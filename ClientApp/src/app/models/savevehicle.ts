import { Contact } from './contact';
import { KeyValuePair } from './keyValuePair';

export interface SaveVehicle {
	id: number;
	makeId: number;
	modelId: number;
	isRegistered: boolean;
	features: number[];
	contact: Contact;
}