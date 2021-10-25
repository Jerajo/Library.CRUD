import { Guid } from "guid-typescript";
import { Author } from "./Author";
import { Category } from "./Category";
import { BookStockByEditions } from "./BookStockByEditions";

export type Book = {
  id: Guid;
  title: string;
  publicationDate: Date;
  categories: Category[];
  authors: Author[];
  bookStockByEditions: BookStockByEditions[];
};

export type BookForm = {
  id: Guid;
  stockId: string;
  title: string;
  publicationDate: Date | null;
  bookEdition: number;
  availableBooks: number;
  borrowedBooks: number;
  categories: Guid[];
  authors: Guid[];
};
