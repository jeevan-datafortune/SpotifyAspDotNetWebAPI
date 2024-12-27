import { UserModel } from "./userModel"

export type AuthResponseModel={
    isSuccess:boolean,
    token:string,
    refreshToken:string,
    user:UserModel,
    expiresIn:Date
}