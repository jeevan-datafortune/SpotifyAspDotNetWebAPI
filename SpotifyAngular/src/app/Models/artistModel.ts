export type ArtistModel={
    id:number|null,
    name:string
}
export type ArtistTableDataSource= ArtistModel &{
    position:number
}