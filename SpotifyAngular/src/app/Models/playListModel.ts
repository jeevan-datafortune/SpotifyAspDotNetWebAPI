import { ImageModel } from "./imageModel"
import { UserModel } from "./userModel"

export type PlaylistModel={
    id:number,
    name:string,
    description:string,
    isPublic:boolean,
    userID:number,
    owner:UserModel,
    images:ImageModel[],
    songsCount:number,
    duration:number
}

export type PlayListDataSource={
    id:number,
    name:string,
    description:string,
    songsCount:number,
    duration:number,
    image:string
}