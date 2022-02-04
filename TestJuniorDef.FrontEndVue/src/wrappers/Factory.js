import BrandWrapper from "./BrandWrapper";
import ProductWrapper from "./ProductWrapper";

const repositories = {
    brands: BrandWrapper,
    products: ProductWrapper,
};

export const Factory = {
    get: name => repositories[name]
  };