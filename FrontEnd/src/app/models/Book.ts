import { Author } from "./Author";
import { Comments } from "./Comment";

export interface Book {
    id?: string;
    name: string;
    description:string;
    authorID: string;
    pictureURL:string;
    publishDate:string;
    author?: Author;
    comments?: Comments[];
    authorName?:string;
  }