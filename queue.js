import {EventEmitter} from 'events';
import {queueProxy} from './proxy';

export default class Queue extends EventEmitter {

	constructor(path) {
		super();
		this.path = path;
	}

	static existsQueue(path) {
		return queueProxy.exists(path, true);
	}

	static createQueue(path) {
		let result = queueProxy.create(path, true)

		if (!result) {
			throw new Error('Queue already exists');
		}

		return new Queue(path);
	}

	static openOrCreateQueue(path) {
		queueProxy.create(path, true);
		return new Queue(path);
	}

	startReceiving() {
		if (this.receiving) {
			throw new Error('Already receiving messages from this queue');
		}

		this.receiving = true;

		return queueProxy.receive({
			path: this.path,
			receive: (message) => {
				this.emit('receive', message);
			}
		});
	}

	send(message, cb) {
		let formattedMessage = message.toString();

		return queueProxy.send({
			path: this.path,
			message: formattedMessage
		}, cb);
	}

	getAllMessages() {
		return queueProxy.list(this.path, true);
	}

	purge() {
		queueProxy.clear(this.path, true);
	}

}
