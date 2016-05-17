import edge from 'edge';

const baseOptions = {
	assemblyFile: 'MSMQLib.dll',
	typeName: 'MSMQLib.MSMQInterface',
};

function getMethod(methodName) {
	return edge.func(Object.assign({}, baseOptions, {methodName}));
}

export var queueProxy = {
	exists: getMethod('ExistsQueue'),
	create: getMethod('CreateQueue'),
	send: getMethod('SendMessage'),
	receive: getMethod('ReceiveMessages'),
	list: getMethod('GetAllMessages'),
	clear: getMethod('PurgeQueue')
};
