import { Contact } from './contact';
import { KeyValuePair } from './keyValuePair';

export interface Vehicle {
	id: number;
	make: KeyValuePair;
	model: KeyValuePair;
	isRegistered: boolean;
	features: KeyValuePair[];
	contact: Contact;
	lastUpdate: string;
}