import BrandWrapper from "./BrandWrapper";
import ProductWrapper from "./ProductWrapper";
import LeedWrapper from "./LeedWrapper";
import CategoryWrapper from "./CategoryWrapper";

const repositories = {
    brands: BrandWrapper,
    products: ProductWrapper,
    leeds: LeedWrapper,
    categories: CategoryWrapper,
};

export const Factory = {
    get: name => repositories[name]
  };