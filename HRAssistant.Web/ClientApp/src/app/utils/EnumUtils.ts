export class EnumUtils {
    public static equals(a: any, b: any, enumType: any): boolean {
        const aIsNumber = Number.isInteger(a);
        const bIsNumber = Number.isInteger(b);
        if (aIsNumber && !bIsNumber) {
            return enumType[a] === b;
        }
        if (bIsNumber && !aIsNumber) {
            return enumType[b] === a;
        }

        return a === b;
    }

}