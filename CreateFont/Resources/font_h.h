#ifndef __FONT_H
#define __FONT_H

#include <stdint.h>

typedef struct {
    uint32_t unicode;
    uint8_t width;
    uint8_t height;
    int8_t offset;
    uint8_t data[];
} Font_TypeDef;

const Font_TypeDef *FONT_GetFont(const uint8_t *font, const char *str, uint8_t *utf8Count);

#endif // __FONT_H
