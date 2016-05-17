import edge from 'edge';
import path from 'path';

const baseOptions = {
	assemblyFile: path.join(__dirname, 'MSMQLib.dll'),
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
