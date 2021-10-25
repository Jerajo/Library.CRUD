<template>
  <div class="container">
    <h1 class="my-4">Book List</h1>
    <button
      id="createButton"
      @click="goToBookForm(null, null)"
      class="btn btn-success mx-auto mb-4"
    >
      Add a new book
    </button>
    <div v-if="loading">
      <h2>Loading books...</h2>
      <b-icon icon="arrow-clockwise" font-scale="7.5" animation="spin"></b-icon>
    </div>
    <div v-else-if="hasBooks === false" class="text-md-center">
      <h2>There are not books on the list yet.</h2>
    </div>
    <div v-else>
      <b-container>
        <!-- Book Header -->
        <b-row cols="5" align-v="center" class="bg-secondary text-light py-2">
          <b-col cols="3" class="text-break">Title</b-col>
          <b-col cols="2" class="text-break">Categories</b-col>
          <b-col cols="2" class="text-break">Authors</b-col>
          <b-col cols="1" class="text-break">Publication</b-col>
          <b-col cols="1" class="text-break">Edition</b-col>
          <b-col cols="1" class="text-break">Available</b-col>
          <b-col cols="1" class="text-break">Borrowed</b-col>
          <b-col cols="1">
            <span class="d-none d-lg-block">Actions</span>
          </b-col>
        </b-row>

        <!-- Book List -->
        <div v-for="book in books" :key="book.id">
          <div v-for="stock in book.bookStockByEditions" :key="stock.id">
            <b-row cols="5" align-v="center" class="py-2">
              <b-col cols="3" class="text-break">
                {{ book.title }}
              </b-col>
              <b-col cols="2" class="text-break">
                <span v-for="category in book.categories" :key="category.id">
                  {{ category.name }}
                </span>
              </b-col>
              <b-col cols="2" class="text-break">
                <span v-for="author in book.authors" :key="author.id">
                  {{ author.firstName }} {{ author.lastName }}
                </span>
              </b-col>
              <b-col cols="1" class="text-break">
                {{ book.publicationDate }}
              </b-col>
              <b-col cols="1" class="text-break">
                {{ stock.bookEdition }}
              </b-col>
              <b-col cols="1" class="text-break">
                {{ stock.availableBooks }}
              </b-col>
              <b-col cols="1" class="text-break">
                {{ stock.borrowedBooks }}
              </b-col>
              <b-col cols="1">
                <b-dropdown variant="outline-secondary" no-caret right>
                  <template #button-content>
                    <b-icon
                      icon="three-dots-vertical"
                      title="Show Actions"
                    ></b-icon>
                  </template>
                  <b-card class="menu-card">
                    <button
                      @click="goToBookForm(book.id, stock.id)"
                      class="btn btn-success w-100 mb-2"
                    >
                      <b-icon icon="pencil-fill"></b-icon>
                      <span class="ms-2">Edit</span>
                    </button>
                    <b-button
                      class="btn btn-danger w-100"
                      block
                      @click="showModal(book.id, stock.id)"
                    >
                      <b-icon icon="trash-fill"></b-icon>
                      <span class="ms-2">Delete</span>
                    </b-button>
                  </b-card>
                </b-dropdown>
              </b-col>
            </b-row>
          </div>
        </div>
      </b-container>
    </div>

    <!-- Delete Modal -->
    <b-modal v-model="modalShow" hide-footer>
      <template #modal-title> Delete book registry </template>
      <div class="d-block text-center my-5">
        <span>Are you sure yo want to delete this registry?</span>
      </div>
      <div class="w-100">
        <b-button class="btn btn-secondary mx-2" @click="modalShow = false">
          Close
        </b-button>
        <b-button class="btn btn-danger mx-2" @click="deleteBook()" block>
          <b-icon icon="trash-fill"></b-icon>
          <span class="ms-2">Delete</span>
        </b-button>
      </div>
    </b-modal>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Book } from "../../models/Book";
import { handleError } from "../../helpers";
import { WebClient, Endpoints } from "../../helpers/WebClient";
import { Guid } from "guid-typescript";

@Component
export default class ClientList extends Vue {
  loading: boolean;
  books: Book[];
  bookId: Guid | null;
  stockId: Guid | null;
  webClient = new WebClient();
  modalShow: boolean;

  constructor() {
    super();

    this.loading = true;
    this.books = [];
    this.webClient = new WebClient();
    this.bookId = null;
    this.stockId = null;
    this.modalShow = false;
  }

  get hasBooks() {
    const result = this.books.length > 0;

    return result;
  }

  mounted(): void {
    this.getBooks();
  }

  getBooks(): void {
    this.webClient
      .GET(Endpoints.Books)
      .then(value => {
        this.books = value.data as Book[];
      })
      .catch(handleError)
      .then(() => {
        this.loading = false;
      });
  }

  goToBookForm(id: Guid | null, sId: Guid) {
    const bookId = id ? id.toString() : null;
    const stockId = sId ? sId.toString() : null;
    const operation = bookId ? "Edit" : "Create";

    this.$router.push({
      path: "/book-form",
      query: { operation, bookId, stockId }
    });
  }

  showModal(bookId: Guid, stockId: Guid) {
    this.bookId = bookId;
    this.stockId = stockId;
    this.modalShow = true;
  }

  deleteBook() {
    this.webClient
      .DELETE(`${Endpoints.Books}/${this.bookId}/${this.stockId}`)
      .then(() => {
        const book = this.books.filter(x => x.id == this.bookId)[0];
        book.bookStockByEditions = book.bookStockByEditions.filter(
          x => x.id !== this.stockId
        );
        this.modalShow = false;
      })
      .catch(handleError);
  }

  clearList(): void {
    this.books = [];
  }
}
</script>

<style lang="scss" scoped>
.menu-card {
  background-color: rgb(212, 212, 212);
}
.addresses {
  background-color: rgb(202, 230, 255);
}
</style>
