export class MessageModel{
	Message? : string;
	IsSuccess? : boolean;
	Data? : any 

	constructor(message? : string, success? : boolean){
		this.Message = message;
		this.IsSuccess = success;
	}
}