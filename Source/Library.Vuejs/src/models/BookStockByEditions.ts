import { Guid } from "guid-typescript";

export type BookStockByEditions = {
  id: Guid;
  bookEdition: number;
  availableBooks: number;
  borrowedBooks: number;
};
