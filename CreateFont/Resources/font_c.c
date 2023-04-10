
#include "font.h"

static uint32_t UTF8_Decode(const char *str, uint8_t *utf8Count) {
    if(*str & 0x80) {
        uint8_t byteCount;
        for(byteCount = 1; byteCount < 6; byteCount++) {
            if(!(*str & (0x80 >> byteCount)))
                break;
        }
        *utf8Count = byteCount;

        uint32_t code = *str & (0xFF >> (byteCount + 1));
        while(--byteCount) {
            str++;
            code <<= 6;
            code |= *str & 0x3F;
        }
        return code;
    }
    *utf8Count = 1;
    return *str;
}

const Font_TypeDef *FONT_GetFont(const uint8_t *font, const char *str, uint8_t *utf8Count) {
    uint32_t unicode = UTF8_Decode(str, utf8Count);
    uint32_t count = *(uint32_t *)font;

    uint32_t r = count - 1;
    uint32_t l = 0;
    while(r >= l) {
        uint32_t mid = l + (r - l) / 2;
        const Font_TypeDef *temp = (const Font_TypeDef *)&font[((uint32_t *)font)[mid + 1]];
        if(temp->unicode == unicode)
            return temp;
        else if(temp->unicode > unicode)
            r = mid - 1;
        else
            l = mid + 1;
    }

    return 0;
}
