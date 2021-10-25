/* eslint-disable no-debugger */

<template>
  <div class="container text-start">
    <h1 class="my-4">{{ title }}</h1>
    <ValidationObserver v-slot="{ handleSubmit }">
      <form action="none" @submit.prevent="handleSubmit(onSubmitForm)">
        <ValidationProvider
          name="Title"
          rules="required|max:100"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Book Title</label>
            <div class="col-md-6">
              <input
                v-model="bookForm.title"
                class="form-control"
                placeholder="Book Title"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Book Publication Date"
          rules="required"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Publication Date</label>
            <div class="col-md-6">
              <b-form-datepicker
                id="publicationDay"
                :min="minDate"
                :max="maxDate"
                :state="isDateValid"
                :show-decade-nav="true"
                locale="en"
                v-model="bookForm.publicationDate"
              ></b-form-datepicker>
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Book Edition"
          rules="required"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Book Edition</label>
            <div class="col-md-6">
              <input
                type="number"
                v-model="bookForm.bookEdition"
                :disabled="isEditing"
                :class="isEditing ? 'form-control disabled' : 'form-control'"
                class="form-control"
                placeholder="E-Mail"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Available Books"
          rules="required"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Available Books</label>
            <div class="col-md-6">
              <input
                type="number"
                v-model="bookForm.availableBooks"
                class="form-control"
                placeholder="Available Books"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Borrowed Books"
          rules="required"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Borrowed Books</label>
            <div class="col-md-6">
              <input
                type="number"
                v-model="bookForm.borrowedBooks"
                :disabled="!isEditing"
                :class="!isEditing ? 'form-control disabled' : 'form-control'"
                class="form-control"
                placeholder="Borrowed Books"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Categories"
          rules="required|min:1"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">
              Categories
            </label>
            <div class="col-md-6">
              <b-form-select
                v-model="bookForm.categories"
                class="form-control"
                :options="categories"
                multiple
                :select-size="4"
              ></b-form-select>
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Authors"
          rules="required|min:1"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">
              Authors
            </label>
            <div class="col-md-6">
              <b-form-select
                v-model="bookForm.authors"
                class="form-control"
                :options="authors"
                multiple
                :select-size="4"
              ></b-form-select>
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <div class="d-flex fle-row">
          <a @click="goBack" class="btn btn-outline-secondary my-3">
            Go Back
          </a>
          <input
            type="submit"
            class="btn btn-outline-primary my-3 mx-3"
            value="Save changes"
          />
        </div>
      </form>
    </ValidationObserver>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Book, BookForm } from "../../models/Book";
import { Author } from "../../models/Author";
import { Category } from "../../models/Category";
import { SelectItem } from "../../models/SelectItem";
import { handleError } from "../../helpers";
import { WebClient, Endpoints } from "../../helpers/WebClient";
import { Guid } from "guid-typescript";

@Component
export default class BookFormulary extends Vue {
  title: string;
  isEditing: boolean;
  minDate: Date;
  maxDate: Date;
  bookForm: BookForm;
  webClient: WebClient;
  bookId: string | undefined;
  stockId: string | undefined;
  categories: SelectItem[];
  authors: SelectItem[];

  constructor() {
    super();

    this.title = "";
    this.isEditing = false;
    this.minDate = new Date("01/01/1900");
    this.maxDate = new Date();
    this.authors = [];
    this.categories = [];

    this.webClient = new WebClient();
    this.bookForm = this.getBookForm();
  }

  get isDateValid(): boolean | null {
    const isValid = this.bookForm.publicationDate != null;
    return isValid ? null : isValid;
  }

  created() {
    this.title = this.$route.query.operation + " Book";

    const bookId = this.$route.query.bookId;
    const stockId = this.$route.query.stockId;

    if (bookId && Guid.isGuid(bookId)) {
      this.isEditing = true;
      this.bookId = bookId as string;
      this.stockId = stockId as string;
      this.fetchBook(this.bookId, this.stockId);
    }

    this.fetchAuthors();
    this.fetchCategories();
  }

  fetchBook(bookId: string, stockId: string) {
    this.webClient
      .GET(`${Endpoints.Books}/${bookId}`)
      .then(value => {
        const book = value.data as Book;
        const stock = book.bookStockByEditions.filter(
          x => x.id.toString() == stockId
        )[0];
        this.bookForm = {
          id: book.id,
          stockId: stockId,
          title: book.title,
          publicationDate: book.publicationDate,
          bookEdition: stock.bookEdition,
          availableBooks: stock.availableBooks,
          borrowedBooks: stock.borrowedBooks,
          categories: book.categories.map(category => category.id),
          authors: book.authors.map(author => author.id)
        };
      })
      .catch(handleError);
  }

  fetchCategories() {
    this.webClient
      .GET(`${Endpoints.Categories}`)
      .then(value => {
        const data = value.data as Category[];
        this.categories = data.map(category => {
          const r = { value: category.id, text: category.name };
          return r;
        });
      })
      .catch(handleError);
  }

  fetchAuthors() {
    this.webClient
      .GET(`${Endpoints.Authors}`)
      .then(value => {
        const data = value.data as Author[];
        this.authors = data.map(author => {
          const r = {
            value: author.id,
            text: `${author.firstName} ${author.lastName}`
          };
          return r;
        });
      })
      .catch(handleError);
  }

  getBookForm(): BookForm {
    const book: BookForm = {
      id: Guid.parse(Guid.EMPTY),
      stockId: "",
      title: "",
      publicationDate: null,
      bookEdition: 0,
      availableBooks: 0,
      borrowedBooks: 0,
      authors: [],
      categories: []
    };

    return book;
  }

  onSubmitForm() {
    console.log(this.bookForm);
    if (this.isEditing) {
      this.webClient
        .PUT(`${Endpoints.Books}/${this.bookId}`, this.bookForm)
        .then(() => {
          this.goBack();
        })
        .catch(handleError);
    } else {
      this.webClient
        .POST(Endpoints.Books, this.bookForm)
        .then(value => {
          const book = value.data as Book;
          this.goBack();
        })
        .catch(handleError);
    }
  }

  goBack() {
    this.$router.back();
  }
}
</script>

<style>
.disabled {
  background-color: lightgrey;
}
.row {
  margin: 0px;
}
</style>
