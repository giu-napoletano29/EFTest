import BrandWrapper from "./BrandWrapper";
import ProductWrapper from "./ProductWrapper";
import LeedWrapper from "./LeedWrapper";

const repositories = {
    brands: BrandWrapper,
    products: ProductWrapper,
    leeds: LeedWrapper,
};

export const Factory = {
    get: name => repositories[name]
  };