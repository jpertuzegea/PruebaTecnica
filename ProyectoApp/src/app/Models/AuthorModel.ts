import { BookModel } from "./BookModel";

export class AuthorModel {

  Id: number;
  IdBook: number;
  FirstName: string;
  LastName: string;
  ListBooks: BookModel[];

}
