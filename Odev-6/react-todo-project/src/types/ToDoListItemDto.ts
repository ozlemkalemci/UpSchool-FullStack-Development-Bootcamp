import { v4 as uuidv4 } from 'uuid';
export  class ToDoListItemDto{
    public Id:string;
    public Task:string;
    public CreatedOn:Date;
    public IsCompleted:boolean;
    constructor() {
    this.Id = uuidv4();
    this.Task= "";
    this.CreatedOn= new Date();
    this.IsCompleted = false;

    }
}