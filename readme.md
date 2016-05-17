# node-msmq

> A MSMQ implementation for node.js

## Install

```
$ npm install --save node-msmq
```

## Usage

### Send a message

Sends a message to a MSMQ queue.

```js
const msmq = require('node-msmq');

var queue = msmq.openOrCreateQueue('.\\Private$\\MyAwesomeQueue');

// Send message to queue
queue.send('Hello from Node.JS!');
```

### Receive messages

Start receiving messages from a queue.

```js
const msmq = require('node-msmq');

var queue = msmq.openOrCreateQueue('.\\Private$\\MyAwesomeQueue');

// Set receive listener callback
queue.on('receive', (msg) => {
  console.log(msg.body);
});

// Start receiving messages from the queue
queue.startReceiving();
```

### Get all messages

Gets all messages without removing them from queue.

```js
const msmq = require('node-msmq');

var queue = msmq.openOrCreateQueue('.\\Private$\\MyAwesomeQueue');
var messages = queue.getAllMessages();
```

### Purge a queue

Clears all messages from the queue.

```js
const msmq = require('node-msmq');

var queue = msmq.openOrCreateQueue('.\\Private$\\MyAwesomeQueue');
queue.purge();
```

## License

MIT © [Marco Barcelos](http://marcobarcelos.com)
