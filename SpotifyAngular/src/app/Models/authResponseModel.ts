import { UserModel } from "./userModel"

export type AuthResponseModel={
    isSuccess:boolean,
    token:string,
    user:UserModel
}