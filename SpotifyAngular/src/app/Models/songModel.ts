import { ArtistModel } from "./artistModel";

export type SongModel={
    id:number,
    name:string,
    uri:string,
    duration:number,
    image:string,
    artists:ArtistModel[],
    addedOn:Date
}