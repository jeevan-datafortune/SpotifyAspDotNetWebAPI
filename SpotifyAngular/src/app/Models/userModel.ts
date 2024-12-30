export type UserModel={
    id:number,
    name:string,
    email:string,
    isActive:boolean,   
    password:string
}

export type UserDataSource=UserModel &{
    position:number
}