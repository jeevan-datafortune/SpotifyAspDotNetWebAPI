import { ArtistModel } from "./artistModel";

export type SongModel={
    id:number,
    name:string,
    uri:string,
    duration:number,
    image?:string,
    artists:ArtistModel[],
    selectedArtists?:number[],
    addedOn?:Date
}

export type SongDataSource={
    position:number,
    id:number,
    name:string,
    uri:string,
    duration:number,
    image:string|null,
    artists:string|null,
    songArtists:ArtistModel[]|null
}