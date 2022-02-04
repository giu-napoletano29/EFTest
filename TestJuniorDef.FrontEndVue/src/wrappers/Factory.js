import BrandWrapper from "./BrandWrapper";

const repositories = {
    brands: BrandWrapper,
};

export const Factory = {
    get: name => repositories[name]
  };